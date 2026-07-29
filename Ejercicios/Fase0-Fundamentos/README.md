# Fase 0: Fundamentos de programacion y C#

Esta fase es el paso previo a todo el resto de MiBiblioteca. Si nunca
programaste antes, o si programaste pero nunca en C#, empeza aca.

## Como usarlo

No hace falta saber de testing ni de frameworks: es un programa de
consola comun que se corre con:

```bash
cd Ejercicios/Fase0-Fundamentos
dotnet run
```

Al correrlo vas a ver una lista de ejercicios marcados `[FALTA]` (todavia
no completados) o `[OK]` (correctos). Abri la carpeta `Ejercicios/` y
completa los archivos en orden, del `01_Variables.cs` al `10_Linq.cs`:
cada uno tiene metodos con un comentario que explica que tienen que
hacer, y una linea `throw new NotImplementedException();` que hay que
reemplazar por tu codigo. Guarda el archivo, corre `dotnet run` de nuevo,
y fijate si el `[FALTA]` paso a `[OK]`.

No pasa nada si dejas ejercicios sin terminar y segui probando: el
programa no se corta, solo te muestra que falta.

## Orden y por que ese orden

1. **Variables y operadores** - la base de todo: como guardar datos.
2. **Condicionales** - como tomar decisiones segun esos datos.
3. **Bucles** - como repetir una accion sin copiar y pegar codigo.
4. **Metodos** - como organizar el codigo en piezas reutilizables.
5. **Colecciones** (`List<T>`, `Dictionary`) - como manejar varios datos a
   la vez. Ya aca aparece el mismo concepto de "generico" (`<T>`) que
   vas a ver en `IRepositoryBase<TEntity, TId>` en la Fase 2.
6. **Clases y objetos** - la base de la Programacion Orientada a Objetos,
   que es como esta escrito TODO MiBiblioteca (y casi todo .NET).
7. **Herencia e interfaces** - como comparten comportamiento distintas
   clases, y como se define un "contrato" (interfaz) sin atarse a una
   implementacion concreta - la base de por que MiBiblioteca esta lleno
   de interfaces (`IBookService`, `IUnitOfWork`, etc.).
8. **Excepciones** - como manejar errores, la base de `ApiExceptionFilter`
   (Fase 9).
9. **async/await** - la base de por que casi todo en MiBiblioteca termina
   en `Async` y devuelve `Task`.
10. **LINQ** - consultas sobre listas, la base de como los repositorios
    de MiBiblioteca buscan datos.

Cuando termines los 10 y veas "56/56 ejercicios correctos", estas listo
para seguir con la Fase 1 del proyecto principal (ver el `README.md` en
la raiz del repositorio).
