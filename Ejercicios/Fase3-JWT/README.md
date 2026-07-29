# Ejercicio: JWT

Version simplificada de la generacion y validacion de tokens que usa
MiBiblioteca de verdad para el login.

## Como usarlo

```bash
cd Ejercicios/Fase3-JWT
dotnet test
```

Vas a ver 5 tests en rojo. Completa `GenerarToken` y
`ValidarYObtenerUsername` en `GeneradorDeTokens.cs` (tienen pistas
detalladas en los comentarios) y volve a correr `dotnet test` hasta que
los 5 pasen.

## Que es un JWT, en una linea

Un string con 3 partes separadas por puntos (`header.payload.signature`)
que prueba una identidad sin que el servidor tenga que guardar sesiones:
toda la info necesaria viaja dentro del token, y la firma garantiza que
nadie lo modifico despues de emitido.

## Que probar y por que (los 5 tests)

- **Tres partes**: confirma que lo que generaste es un JWT valido en su
  forma (no cualquier string).
- **Token valido**: el camino feliz - generar y validar con la misma
  clave tiene que funcionar.
- **Clave incorrecta**: simula a alguien que no conoce el secreto del
  servidor intentando validar un token - tiene que fallar.
- **Token vencido**: un token con fecha de expiracion pasada no debe ser
  aceptado, aunque este perfectamente firmado.
- **Token corrupto**: si se modifica un solo caracter del token despues
  de firmado, la validacion tiene que rechazarlo.

## Que tiene que ver esto con MiBiblioteca de verdad

Cuando termines, mira
`Infrastructure/MiBiblioteca.Identity/JwtTokenGenerator.cs` en la raiz
del repositorio: usa las mismas clases
(`SymmetricSecurityKey`, `SigningCredentials`, `JwtSecurityToken`), solo
que con mas claims (`sub` con el id del usuario, `jti` como identificador
unico del token) y usando el algoritmo real de expiracion configurado en
`appsettings.json` en vez de un numero fijo de minutos.
