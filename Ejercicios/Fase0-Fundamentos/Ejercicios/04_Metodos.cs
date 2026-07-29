namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 4: Metodos (parametros, retorno, reutilizacion)
    // ============================================================
    // Un METODO es un bloque de codigo con nombre que podemos "llamar"
    // (ejecutar) desde otros lugares, tantas veces como haga falta, sin
    // repetir el codigo. Ya veniamos usando metodos en los ejercicios
    // anteriores (Sumar, EsPar, etc.) - ahora el foco es armar metodos que
    // se apoyan unos en otros, como pasa todo el tiempo en MiBiblioteca
    // (por ejemplo, BookService.CreateAsync llama a un metodo privado
    // EnsureIsbnIsAvailableAsync en vez de repetir esa logica).
    public static class Ej04Metodos
    {
        // Metodo auxiliar YA COMPLETO, para que uses los de abajo.
        public static bool EsPrimo(int numero)
        {
            if (numero < 2) return false;
            for (int divisor = 2; divisor * divisor <= numero; divisor++)
            {
                if (numero % divisor == 0) return false;
            }
            return true;
        }

        // Debe devolver una lista con todos los numeros primos entre 2 y n
        // (incluido n). Pista: recorré del 2 al n y usá EsPrimo de arriba
        // para decidir si agregar cada numero a la lista.
        public static List<int> PrimosHasta(int n)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver cuantos numeros primos hay entre 2 y n.
        // Pista: no hace falta reescribir la logica de buscarlos - llama a
        // PrimosHasta y fijate cuantos elementos tiene la lista que te
        // devuelve (.Count).
        public static int ContarPrimosHasta(int n)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el precio final con descuento aplicado.
        // "porcentajeDescuento" viene como un numero entre 0 y 100
        // (por ejemplo 20 significa 20% de descuento).
        public static double PrecioConDescuento(double precioOriginal, double porcentajeDescuento)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el precio final aplicando DOS descuentos seguidos
        // (uno atras del otro, no sumados). Por ejemplo, 20% y despues 10%
        // sobre $100 da $100 * 0.8 * 0.9 = $72.
        // Pista: no repitas la formula - llama dos veces a
        // PrecioConDescuento de arriba.
        public static double PrecioConDobleDescuento(double precioOriginal, double descuento1, double descuento2)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
