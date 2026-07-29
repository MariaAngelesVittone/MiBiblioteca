# MiBiblioteca

Proyecto para aprender a programar en .NET desde cero, cubriendo todo el
temario de la catedra: Clean Architecture, Repository Generico, JWT,
Testing (unitario/integracion/TDD), consumo de APIs externas con
resiliencia (Polly), CI/CD, Federated Identity y Cloud Services (teoria),
AutoMapper, Filtros y Middlewares, y buenas practicas/SOLID.

Es una API de gestion de biblioteca personal: usuarios se registran,
agregan libros (a mano o autocompletando datos por ISBN desde Open
Library), y llevan un registro de lectura.

## Por donde empezar

**Si nunca programaste antes, o nunca programaste en C#:** empeza por
[`Ejercicios/Fase0-Fundamentos`](Ejercicios/Fase0-Fundamentos/README.md).
Es un programa de consola con 10 ejercicios progresivos (variables,
condicionales, bucles, clases, excepciones, async/await, LINQ...) que se
autoverifican corriendo `dotnet run`. No hace falta entender testing ni
frameworks para usarlo.

**Si ya sabes programar pero es tu primera vez con .NET/C#, o si ya
terminaste la Fase 0:** segui con el proyecto principal, en el orden de
la tabla de abajo. Cada fase tiene su commit correspondiente en el
historial de git con una explicacion de que se agrego y por que.

## Orden de estudio

| Fase | Tema | Donde esta el codigo |
|------|------|------------------------|
| 0 | Fundamentos de C# (si te hace falta) | `Ejercicios/Fase0-Fundamentos/` |
| 1 | Clean Architecture (Domain/Application/Infrastructure/Presentation) | `Core/`, `Infrastructure/`, `Presentation/` |
| 2 | Repository Generico | `Core/MiBiblioteca.Application/Interfaces/Repositories/`, `Infrastructure/MiBiblioteca.Persistence/Repository/` — practica guiada en `Ejercicios/Fase2-RepositoryGenerico/` |
| 3 | JWT Authentication | `Infrastructure/MiBiblioteca.Identity/` — practica guiada en `Ejercicios/Fase3-JWT/` |
| 4 | Testing (unitario, integracion, AAA, TDD) | `Tests/` — practica guiada en `Ejercicios/Fase4-Testing/` |
| 5 | API externa (Open Library) + Polly | `Infrastructure/MiBiblioteca.ExternalServices/` |
| 6 | CI/CD con GitHub Actions | `.github/workflows/ci.yml` |
| 7 | Federated Identity y Cloud Services (teoria) | `docs/Fase7-FederatedIdentity-CloudServices.md` |
| 8 | AutoMapper | `Core/MiBiblioteca.Application/Mapping/` |
| 9 | Filtros y Middlewares | `Presentation/MiBiblioteca.WebAPI/Filters/`, `.../Middlewares/` |
| 10 | Buenas practicas, convenciones, SOLID | `docs/Fase10-BuenasPracticas.md` |

Las fases 2, 3 y 4 tienen ademas un **ejercicio guiado** en `Ejercicios/`
con espacios para completar vos mismo (marcados `// TODO: completar`) y
tests que te dicen si lo lograste - antes de mirar como quedo resuelto en
el codigo real del proyecto principal, probalo vos.

## Como correr el proyecto principal

```bash
# Toda la suite de tests (unitarios + integracion)
dotnet test

# La API con Swagger
cd Presentation/MiBiblioteca.WebAPI
dotnet run
# abrir http://localhost:5255/swagger
```

## Como correr un ejercicio guiado

Cada carpeta bajo `Ejercicios/` tiene su propio README con instrucciones
especificas. En general:

```bash
cd Ejercicios/Fase2-RepositoryGenerico.Tests   # o el que corresponda
dotnet test
```

Vas a ver los tests en rojo hasta que completes los TODO del archivo
correspondiente.

## Estructura del proyecto principal

```
Core/
  MiBiblioteca.Domain/          Entidades, sin dependencias de nada mas
  MiBiblioteca.Application/     Casos de uso, interfaces, DTOs, mapeos
Infrastructure/
  MiBiblioteca.Persistence/     EF Core + Sqlite
  MiBiblioteca.Identity/        JWT
  MiBiblioteca.ExternalServices/ Open Library + Polly
Presentation/
  MiBiblioteca.WebAPI/          Controllers, Filtros, Middlewares, Program.cs
Tests/
  MiBiblioteca.UnitTests/       Mocks (Moq), aislados
  MiBiblioteca.IntegrationTests/ Sqlite real, TestServer real
docs/                           Material teorico (Fase 7 y Fase 10)
Ejercicios/                     Fase 0 y ejercicios guiados de las fases mas dificiles
```
