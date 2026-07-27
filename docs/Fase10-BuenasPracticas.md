# Fase 10: Buenas practicas, convenciones y principios

Esta fase no agrega funcionalidad nueva: es una revision del codigo que ya
existe, buscando donde se cumplen (o no) las convenciones y principios de
diseño de las fases anteriores. Dos cosas de esta revision terminaron en
cambios de codigo reales (ver seccion 3); el resto queda documentado para
poder discutirlo.

## 1. Convenciones de nombres

Revisando los ~50 archivos `.cs` del proyecto, la convencion se mantuvo
consistente en toda la solucion:

- Campos privados: `_camelCase` (`AuthService._unitOfWork`,
  `BooksController._bookService`, `RepositoryBase._context`).
- Metodos que hacen I/O real (llaman a la base, a Open Library, etc.)
  siempre terminan en `Async` y devuelven `Task`/`Task<T>`
  (`GetByIdAsync`, `SaveChangesAsync`, `RegisterAsync`).
- Interfaces siempre con prefijo `I` (`IUnitOfWork`, `IBookService`,
  `IJwtTokenGenerator`).

Una excepcion que a primera vista parece un error pero no lo es:
`JwtTokenGenerator.GenerateToken` no tiene el sufijo `Async`. Es correcto:
ese metodo no hace ningun I/O (solo arma un JWT en memoria con
`SymmetricSecurityKey`), asi que ponerle `Async` seria mentir sobre lo que
hace. La convencion `*Async` es sobre el comportamiento (hay una espera
real de por medio), no sobre "toda operacion relacionada a datos".

## 2. SOLID, con ejemplos reales de este mismo repo (no de ejemplos inventados)

- **S (Responsabilidad unica)**: `OpenLibraryClient` (Fase 5) solo sabe
  llamar a Open Library y parsear la respuesta a `BookMetadataDto`. No
  guarda nada en la base, no arma la entidad `Book` - eso pasa un nivel
  mas arriba, en `BookService` (ver seccion 3).
- **O (Abierto/cerrado)**: `RepositoryBase<TEntity, TId>` (Fase 2) no se
  toca cuando `BookRepository` o `UserRepository` necesitan un metodo
  propio (`GetByIsbnAsync`, `GetByUsernameAsync`): lo agregan extendiendo
  la interfaz especifica (`IBookRepository : IRepositoryBase<Book, Guid>`),
  sin modificar la clase base generica.
- **L (Sustitucion de Liskov)**: en cualquier lugar donde se espera un
  `IRepositoryBase<TEntity, TId>` (por ejemplo dentro de `UnitOfWork`),
  cualquier repositorio concreto lo puede reemplazar sin sorpresas de
  comportamiento - todos respetan el mismo contrato async.
- **I (Segregacion de interfaces)**: en vez de un `IRepository` gigante con
  metodos de libros, usuarios y reading entries mezclados, cada entidad
  tiene su propia interfaz chica (`IBookRepository`, `IUserRepository`,
  `IReadingEntryRepository`), cada una agregando solo lo que le hace falta
  a `IRepositoryBase`.
- **D (Inversion de dependencias)**: `AuthService` y `BookService` (capa
  Application) dependen de `IUnitOfWork`/`IJwtTokenGenerator`/
  `IBookMetadataProvider` (abstracciones), nunca de `MiBibliotecaContext`,
  `JwtTokenGenerator` u `OpenLibraryClient` (las implementaciones
  concretas viven en Infrastructure y se conectan recien en
  `ServiceExtensions.cs` de cada capa).

## 3. Lo que la revision encontro y se corrigio de verdad

**`BooksController` no seguia el mismo patron que `AuthController`.**
`AuthController` ya delegaba toda su logica a `IAuthService` y traducia
excepciones a respuestas HTTP con un `try/catch`. `BooksController`, en
cambio, tenia el chequeo de ISBN duplicado y el armado de la entidad
`Book` directo en el controller, inyectando `IUnitOfWork` +
`IBookMetadataProvider` + `IMapper` ahi mismo. Dos controllers de la misma
API con dos estilos distintos de capas es inconsistente y hace mas dificil
de mantener (¿donde busco la logica de negocio de "libros" la proxima
vez?).

Se soluciono agregando `IBookService`/`BookService`
(`Core/MiBiblioteca.Application/Services/BookService.cs`), que ahora
concentra: el chequeo de ISBN duplicado (una sola vez, ya no duplicado
entre `Create` y `CreateFromIsbn`), el armado de la entidad y el mapeo a
DTO. `BooksController` quedo tan fino como `AuthController`: recibe la
request, llama al service, y atrapa `InvalidOperationException` para
devolver `BadRequest` - exactamente el mismo patron en los dos
controllers. Se agrego `BookServiceTests.cs` con la misma estructura AAA
que ya tenia `AuthServiceTests`.

**Los metodos sync de `IRepositoryBase` no los llamaba nadie.**
`GetById`, `Get`, `GetList`, `GetAll` y `Count` (sin `Async`) estaban
implementados en `RepositoryBase` pero ni un solo controller o servicio
los usaba - toda la solucion usa las variantes `*Async`. No se borraron
(quedan para completar el ejemplo generico que muestra el apunte de la
catedra), pero se documento por que no hay que usarlos de verdad en un
endpoint: EF Core es async de punta a punta, y una llamada sync bloquea un
thread del pool esperando I/O de base de datos en vez de liberarlo - con
carga concurrente real eso agota el thread pool.

## 4. Un punto que se dejo como discusion, sin cambiar codigo

`IJwtTokenGenerator.GenerateToken(User user)` recibe la entidad `User`
completa (`Domain.Entities.User`) en vez de, por ejemplo, un DTO chico con
solo el id y el username. Esto hace que `MiBiblioteca.Identity` conozca la
forma completa de la entidad `Domain`, cuando en teoria solo necesita dos
o tres claims para armar el token.

No es una violacion de Clean Architecture (Infrastructure SI puede
depender de Domain, la flecha de dependencias apunta hacia adentro, nunca
al reves) y cambiarlo agregaria una clase nueva (`TokenSubject` o similar)
solo para reducir un acoplamiento menor, sin resolver ningun bug real. Se
deja documentado como el tipo de trade-off que hay que evaluar caso por
caso: no toda reduccion de acoplamiento posible vale la pena hacerla.
