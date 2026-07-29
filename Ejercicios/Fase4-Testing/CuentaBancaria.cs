namespace Fase4Testing
{
    // ============================================================
    // EJERCICIO: Testing (AAA y TDD)
    // ============================================================
    // A diferencia de los ejercicios anteriores, aca el enunciado de que
    // debe hacer cada metodo esta en CuentaBancariaTests.cs, no en un
    // comentario de este archivo. Los TESTS son la especificacion - esa
    // es la idea de TDD (Test-Driven Development): el test se escribe
    // primero, describe el comportamiento esperado, y el codigo se
    // escribe recien despues, con el unico objetivo de hacer pasar ese
    // test.
    //
    // Flujo recomendado:
    //   1. Corré "dotnet test" y mira los tests en rojo (fallan).
    //   2. Abri CuentaBancariaTests.cs y leé el primer test que falla.
    //   3. Volvé aca e implementa lo MINIMO para que ese test pase.
    //   4. Corré "dotnet test" de nuevo. Repetí con el siguiente test.
    //
    // Este es el mismo ciclo rojo-verde que se demuestra en
    // Tests/MiBiblioteca.UnitTests/Domain/ReadingEntryTests.cs del
    // proyecto principal.
    public class CuentaBancaria
    {
        public decimal Saldo { get; private set; }

        public CuentaBancaria(decimal saldoInicial)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        public void Depositar(decimal monto)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        public void Retirar(decimal monto)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Pista para esta: no repitas la logica de validacion de saldo -
        // llama a los metodos que ya escribiste arriba.
        public void TransferirA(CuentaBancaria destino, decimal monto)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
