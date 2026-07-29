# Ejercicio: Repository Generico

Version simplificada, sin base de datos, del patron que MiBiblioteca usa
para todo: un repositorio generico que sirve para cualquier tipo de
entidad, sin repetir codigo por cada una.

## Como usarlo

1. Abri `RepositorioEnMemoria.cs` y completa los 5 metodos marcados con
   `// TODO: completar`. Cada uno tiene una pista.
2. Corré los tests (ya estan escritos, no hace falta tocarlos):

```bash
cd Ejercicios/Fase2-RepositoryGenerico.Tests
dotnet test
```

3. Vas a ver tests en rojo (fallando) al principio. A medida que
   completes cada metodo de `RepositorioEnMemoria`, mas tests van a
   pasar. Esto ES la practica de TDD que se explica en la Fase 4 del
   proyecto principal: los tests se escriben primero, y el codigo se
   escribe para hacerlos pasar.

## Que tiene que ver esto con MiBiblioteca de verdad

| Aca (ejercicio)              | En MiBiblioteca (real)                                          |
|-------------------------------|------------------------------------------------------------------|
| `IRepositorio<TEntity, TId>`   | `IRepositoryBase<TEntity, TId>` (Core/MiBiblioteca.Application)   |
| `RepositorioEnMemoria`         | `RepositoryBase` (Infrastructure/MiBiblioteca.Persistence)        |
| `Dictionary<TId, TEntity>`     | `DbSet<TEntity>` de Entity Framework                              |
| `IConId<TId>`                  | `BaseEntity` (tiene `Id`, ademas de fechas y borrado logico)      |

Cuando termines, mira el archivo real
`Infrastructure/MiBiblioteca.Persistence/Repository/RepositoryBase.cs` en
la raiz del repositorio y compara: la forma es la misma, solo cambia que
adentro hay una base de datos real en vez de un diccionario.
