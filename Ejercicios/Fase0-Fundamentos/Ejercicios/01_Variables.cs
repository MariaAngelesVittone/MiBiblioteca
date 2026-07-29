namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 1: Variables, tipos de datos y operadores
    // ============================================================
    // Una VARIABLE es un espacio con nombre donde guardamos un dato para
    // usarlo despues. Pensala como una caja con una etiqueta: la etiqueta
    // es el nombre, y adentro va el valor.
    //
    // En C# hay que decir de que TIPO es lo que va a ir en esa caja:
    //   int      -> numeros enteros (1, -5, 100)
    //   double   -> numeros con coma decimal (3.14, -0.5)
    //   string   -> texto ("hola", "Mi Biblioteca")
    //   bool     -> verdadero o falso (true, false)
    //
    // Los OPERADORES combinan valores: + - * / (matematicos),
    // == != < > <= >= (de comparacion, devuelven bool),
    // && || ! (logicos: "y", "o", "no").
    //
    // Reemplaza cada "throw new NotImplementedException()" por el codigo
    // que haga que el metodo cumpla lo que dice el comentario de arriba.
    public static class Ej01Variables
    {
        // Debe devolver la suma de "a" y "b".
        public static int Sumar(int a, int b)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el area de un rectangulo (base * altura).
        public static double AreaRectangulo(double baseRectangulo, double altura)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver un saludo con el formato exacto: "Hola, <nombre>!"
        // Pista: se puede armar un string interpolado con $"Hola, {nombre}!"
        public static string Saludar(string nombre)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver true si "numero" es par, false si es impar.
        // Pista: el operador % (modulo) da el resto de una division.
        // Por ejemplo, 7 % 2 da 1 (7 es impar), 8 % 2 da 0 (8 es par).
        public static bool EsPar(int numero)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver true solo si la persona puede votar en Argentina:
        // edad mayor o igual a 16.
        public static bool PuedeVotar(int edad)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
