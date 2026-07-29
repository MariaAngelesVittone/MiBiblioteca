namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 9: async / await y Task
    // ============================================================
    // Casi todos los metodos de MiBiblioteca terminan en "Async" y
    // devuelven Task o Task<T> (GetByIdAsync, SaveChangesAsync,
    // RegisterAsync...). Un Task representa un trabajo que puede tardar
    // (leer de una base de datos, llamar a una API por HTTP) SIN bloquear
    // el thread mientras se espera - queda libre para atender otra cosa
    // mientras tanto. "await" es el punto donde el codigo dice "pausa aca
    // hasta que esto termine, sin trabar todo lo demas".
    //
    // Para practicar sin depender de una base de datos real, estos
    // ejercicios usan Task.Delay(milisegundos), que simula una espera
    // (como si fuera una llamada lenta a una API) sin hacer nada de
    // verdad.
    public static class Ej09AsyncAwait
    {
        // Debe esperar "milisegundos" (con Task.Delay) y despues devolver
        // el doble de "numero". Pista: el metodo tiene que ser
        // "async Task<int>", y adentro usar
        // "await Task.Delay(milisegundos);" antes del "return".
        public static async Task<int> DuplicarLuegoDeEsperar(int numero, int milisegundos)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Simula "consultar" el precio de un libro (esperando 50ms) y
        // devuelve el precio con el descuento aplicado. Pista: podes
        // reusar la formula de Ej04Metodos.PrecioConDescuento despues de
        // esperar.
        public static async Task<double> ConsultarPrecioConDescuentoAsync(double precioOriginal, double porcentajeDescuento)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe "consultar" (simular con Task.Delay) las dos operaciones EN
        // PARALELO, no una despues de la otra, y devolver la suma de sus
        // resultados. Pista: Task.WhenAll permite esperar varias tareas a
        // la vez, en vez de hacer "await" de a una (que las correria en
        // secuencia y tardaria el doble).
        public static async Task<int> SumarDosOperacionesEnParaleloAsync(int a, int b)
        {
            // TODO: completar
            // Pista:
            //   var tarea1 = DuplicarLuegoDeEsperar(a, 30);
            //   var tarea2 = DuplicarLuegoDeEsperar(b, 30);
            //   await Task.WhenAll(tarea1, tarea2);
            //   return tarea1.Result + tarea2.Result;
            throw new NotImplementedException();
        }
    }
}
