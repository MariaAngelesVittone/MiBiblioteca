namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 8: Manejo de excepciones (try / catch / throw)
    // ============================================================
    // Una EXCEPCION es un error que interrumpe la ejecucion normal del
    // programa. "throw" la lanza, "try/catch" la atrapa para reaccionar
    // en vez de que el programa se caiga. Esto es lo que hace
    // ApiExceptionFilter en MiBiblioteca (Fase 9): atrapa cualquier
    // excepcion no manejada y devuelve una respuesta JSON prolija en vez
    // de que la API explote mostrando un stack trace crudo al cliente.
    public static class Ej08Excepciones
    {
        // Debe devolver a / b, pero si b es 0 debe lanzar
        // DivideByZeroException con el mensaje "No se puede dividir por cero."
        // (en vez de la excepcion que tira .NET solo, que trae otro texto).
        public static double DividirSeguro(double a, double b)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe lanzar ArgumentOutOfRangeException si "edad" es negativa o
        // mayor a 120 (cualquier mensaje sirve), y devolver la misma edad
        // si es valida. Es el mismo patron que ReadingEntry.MarkAsRead usa
        // en MiBiblioteca para validar que el rating este entre 1 y 5.
        public static int ValidarEdad(int edad)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Recibe una operacion que puede fallar (por ejemplo,
        // DividirSeguro con b=0) y debe: ejecutarla, y si lanza CUALQUIER
        // excepcion, devolver "valorPorDefecto" en vez de dejar que la
        // excepcion siga subiendo. Es el mismo concepto que usa Polly
        // (Fase 5) para reintentar o dar una respuesta alternativa cuando
        // una llamada HTTP falla.
        public static double EjecutarConValorPorDefecto(Func<double> operacion, double valorPorDefecto)
        {
            // TODO: completar
            // Pista: try { return operacion(); } catch { return valorPorDefecto; }
            throw new NotImplementedException();
        }
    }
}
