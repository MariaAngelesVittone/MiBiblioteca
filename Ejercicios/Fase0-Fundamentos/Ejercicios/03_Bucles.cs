namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 3: Bucles (for, while, foreach)
    // ============================================================
    // Un bucle repite un bloque de codigo varias veces, sin que tengamos
    // que escribirlo a mano cada vez.
    //
    //   for (int i = 0; i < 10; i++) { ... }     -> cuando sabemos cuantas
    //                                                 vueltas dar (o como
    //                                                 calcularlas)
    //   while (condicion) { ... }                -> repite mientras la
    //                                                 condicion sea true
    //   foreach (var item in coleccion) { ... }   -> recorre cada elemento
    //                                                 de una lista/array
    public static class Ej03Bucles
    {
        // Debe devolver la suma de todos los numeros enteros de 1 a n
        // (incluido n). Por ejemplo, SumaHasta(5) = 1+2+3+4+5 = 15.
        public static int SumaHasta(int n)
        {
            // TODO: completar (usa un for o un while)
            throw new NotImplementedException();
        }

        // Debe devolver el factorial de n (n! = n * (n-1) * ... * 1).
        // Por ejemplo, Factorial(4) = 4*3*2*1 = 24. Factorial(0) = 1.
        public static long Factorial(int n)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver cuantos numeros PARES hay en la lista.
        // Pista: recorre la lista con foreach y contá los que cumplen la
        // condicion (podes reusar Ej01Variables.EsPar).
        public static int ContarPares(List<int> numeros)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el numero mas grande de la lista, sin usar
        // metodos ya armados como .Max() - recorré la lista a mano
        // comparando elemento por elemento.
        public static int Maximo(List<int> numeros)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
