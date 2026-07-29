# Ejercicio: Testing (AAA y TDD)

A diferencia de los otros ejercicios, aca el enunciado no esta en
comentarios de la clase a implementar: esta en los TESTS
(`CuentaBancariaTests.cs`). Esa es la practica central de este ejercicio.

## Como usarlo

```bash
cd Ejercicios/Fase4-Testing
dotnet test
```

1. Vas a ver 7 tests en rojo.
2. Abri `CuentaBancariaTests.cs`, leé el primero (`Constructor_AsignaElSaldoInicial`),
   y anda a `CuentaBancaria.cs` a escribir el minimo codigo para que pase.
3. Corré `dotnet test` de nuevo. Repetí con el siguiente test que
   falle, de a uno.
4. Cuando los 7 esten en verde, agrega vos mismo los 2 tests que se
   piden al final de `CuentaBancariaTests.cs` (son escenarios sin probar
   todavia). Esa parte no se autoverifica - la practica es justamente
   escribir el test y confirmar que detecta el problema si rompes la
   implementacion a proposito.

## Por que este orden (tests -> codigo, no codigo -> tests)

Esto es TDD (Test-Driven Development), tal como se explica en la Fase 4
del proyecto principal. La ventaja de escribir el test primero es que el
test te obliga a pensar el comportamiento ANTES de escribir la
implementacion, y te queda una prueba automatica de que no rompiste nada
la proxima vez que toques ese codigo - podes compararlo con
`Tests/MiBiblioteca.UnitTests/Domain/ReadingEntryTests.cs` en la raiz del
repositorio, que sigue exactamente este mismo flujo con el metodo
`ReadingEntry.MarkAsRead`.
