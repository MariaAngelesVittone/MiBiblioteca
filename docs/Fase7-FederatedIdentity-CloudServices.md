# Fase 7: Federated Identity y Cloud Services (teoria)

Esta fase es solo teorica, sin cuenta de Azure real. La idea es entender
los conceptos y como encajarian en la arquitectura que ya tiene
MiBiblioteca, no desplegar nada de verdad.

## 1. Federated Identity

### El problema que resuelve

Hoy, `AuthService` + `JwtTokenGenerator` (Fase 3) hacen dos cosas a la vez:

1. Verifican la identidad del usuario (usuario/password contra nuestra
   propia base de datos, con BCrypt).
2. Emiten un token firmado con NUESTRA clave (`SymmetricSecurityKey`).

Esto significa que nosotros somos el "Identity Provider" (IdP): guardamos
passwords, somos responsables de que no se filtren, y cada app nueva que
quiera confiar en nuestros usuarios tendria que confiar en nuestra clave.

**Federated Identity** es delegar el paso 1 (y a veces el 2) a un
proveedor externo de confianza -Microsoft Entra ID (antes Azure AD),
Google, Auth0- usando un protocolo estandar como OpenID Connect (OIDC),
que es OAuth2 + una capa de identidad encima. En vez de validar
usuario/password nosotros mismos, redirigimos al usuario al IdP, el IdP lo
autentica, y nos devuelve un token firmado por EL, que nosotros solo
validamos (no emitimos).

### Como cambiaria el codigo

Lo interesante de Clean Architecture aca: este cambio queda **encapsulado
en `MiBiblioteca.Identity`**. `Application` y `Domain` no se enteran, y los
controllers siguen usando `[Authorize]` igual que ahora.

Hoy, en `MiBiblioteca.Identity/ServiceExtensions.cs`, configuramos el
`TokenValidationParameters` con nuestra propia `IssuerSigningKey`:

```csharp
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key), // la clave es NUESTRA
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ...
    };
});
```

Con Microsoft Entra ID como IdP federado, en lugar de una clave simetrica
propia, se valida contra la metadata publica del tenant (que expone las
claves publicas del IdP y las rota solas):

```csharp
// Con el paquete Microsoft.Identity.Web (envuelve AddJwtBearer):
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        options.Authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
        options.Audience = clientId; // el App Registration de MiBiblioteca en Entra
        // Ya no hace falta IssuerSigningKey: la libreria descarga las claves
        // publicas del IdP automaticamente desde su endpoint de metadata
        // (".well-known/openid-configuration") y las cachea/rota sola.
    });
```

Ya no existiria `IJwtTokenGenerator` de nuestro lado (no emitimos tokens),
ni `PasswordHash` en `User` (no guardamos passwords: el usuario se loguea
en Entra, no en MiBiblioteca). El `User` de nuestro dominio pasaria a
identificarse por el `sub`/`oid` (subject/object id) que viene en el token
del IdP, no por una fila de `Users` con password propia.

### Un ejemplo de federated identity que SI se puede mostrar sin cuenta de Azure

El propio pipeline de CI/CD de la Fase 6 (`.github/workflows/ci.yml`) es
un buen lugar para ver federated identity aplicada a maquina-a-maquina, no
solo a usuarios humanos. La forma vieja (y menos segura) de que un GitHub
Action se autentique contra Azure es guardar un secret de larga duracion
(`AZURE_CREDENTIALS`, un service principal con password) en GitHub. La
forma moderna es **workload identity federation**: se le dice a Entra ID
"confia en los tokens OIDC que emite GitHub Actions para ESTE repo
especifico", sin guardar ningun secreto:

```yaml
# Ejemplo de como se veria un job de deploy agregado a ci.yml,
# usando OIDC en vez de un secret guardado:
deploy:
  needs: build-and-test
  runs-on: ubuntu-latest
  permissions:
    id-token: write   # necesario para que GitHub emita el token OIDC
    contents: read
  steps:
    - uses: actions/checkout@v4
    - uses: azure/login@v2
      with:
        client-id: ${{ secrets.AZURE_CLIENT_ID }}
        tenant-id: ${{ secrets.AZURE_TENANT_ID }}
        subscription-id: ${{ secrets.AZURE_SUBSCRIPTION_ID }}
        # sin "client-secret": azure/login intercambia el token OIDC de
        # GitHub por un access token de Azure, confiando en la federacion
        # configurada de antemano en el App Registration de Entra ID.
    - uses: azure/webapps-deploy@v3
      with:
        app-name: mibiblioteca-api
        package: ./publish
```

Aca GitHub Actions es el "usuario" federado: Entra ID nunca ve ni guarda
un password de GitHub, solo confia en un token que GitHub firma para ese
repo/branch puntual. Es el mismo concepto que loguearse con Google, pero
entre dos sistemas en vez de entre un humano y un sistema.

## 2. Cloud Services (Azure) - teoria

Donde encajaria cada pieza de Azure en la arquitectura actual:

### Azure App Service (hosting)

Reemplaza a correr `dotnet MiBiblioteca.WebAPI.dll` en una maquina propia.
Es un PaaS: se le sube el output de `dotnet publish` y Azure maneja el
proceso, el reinicio si se cae, TLS, y el escalado. Para el deploy real
hace falta un paso de `dotnet publish` agregado al workflow de CI antes
del `azure/webapps-deploy` de arriba.

### Azure SQL Database (persistencia)

Hoy `MiBiblioteca.Persistence` usa Sqlite (`UseSqlite`). Migrar a Azure SQL
seria cambiar **una linea** en
`Persistence/ServiceExtensions.cs`:

```csharp
// De:
options.UseSqlite(configuration.GetConnectionString("MiBibliotecaDb"));
// A:
options.UseSqlServer(configuration.GetConnectionString("MiBibliotecaDb"));
```

Nada en `Domain`, `Application` ni en los controllers cambiaria: es
exactamente el punto de la Clean Architecture y del patron Repository -
`IUnitOfWork`/`IRepositoryBase` no saben ni les importa que motor de base
de datos hay atras.

### Azure Key Vault (secretos)

Hoy el `JwtSettings:SecretKey` vive en `appsettings.json` (o en
`appsettings.Development.json`, fuera de git). En un despliegue real ese
secreto -y el connection string de Azure SQL- no deberian estar ahi, sino
en Key Vault, y la app los lee en el arranque via
`builder.Configuration.AddAzureKeyVault(...)`, autenticandose con Managed
Identity (otra forma de identity federada: el App Service tiene su propia
identidad en Entra ID, sin ningun secreto guardado en la app misma).

### Application Insights (observabilidad)

Se conecta justo donde ya tenemos el gancho: `RequestTimingMiddleware`
(Fase 9) hoy loguea a `ILogger`, que en produccion se puede redirigir a
Application Insights agregando el SDK y un logging provider, sin tocar el
middleware en si. Ahi se verian los mismos datos (metodo, path, status,
tiempo de respuesta) pero con dashboards, alertas y busqueda historica.

### Resumen de la capa que cambia por cada servicio

| Servicio Azure          | Que reemplaza                          | Capa afectada |
|-------------------------|------------------------------------------|---------------|
| App Service             | `dotnet run` / IIS local                  | Ninguna (infraestructura) |
| Azure SQL Database      | Sqlite                                    | `MiBiblioteca.Persistence` |
| Microsoft Entra ID      | `JwtTokenGenerator` propio                | `MiBiblioteca.Identity` |
| Key Vault               | `appsettings.json` con secretos           | Configuracion, no codigo |
| Application Insights    | Logs de consola                           | Ninguna (mismo `ILogger`) |

Esta es la ventaja concreta de haber separado el proyecto en capas desde
la Fase 1: migrar a la nube es, en su mayoria, cambiar configuracion e
implementaciones de Infrastructure, no reescribir Domain o Application.
