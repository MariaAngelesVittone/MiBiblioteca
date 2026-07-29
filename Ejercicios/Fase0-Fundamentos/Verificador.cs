using System.Collections;

namespace Fase0Fundamentos
{
    // Este archivo es "infraestructura" del ejercicio, no hace falta
    // entenderlo para aprender C# - solo corre los ejercicios y te dice
    // si el resultado es el esperado. Podes ignorarlo tranquilamente e ir
    // directo a la carpeta Ejercicios/.
    public static class Verificador
    {
        private static int _total;
        private static int _correctos;

        public static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== {texto} ===");
            Console.ResetColor();
        }

        // "obtenerValor" es una funcion, no un valor ya calculado: asi, si tu
        // metodo todavia tira NotImplementedException (porque no lo
        // completaste), lo podemos atrapar y seguir probando el resto de los
        // ejercicios en vez de que se corte todo el programa.
        public static void Check<T>(string enunciado, T esperado, Func<T> obtenerValor)
        {
            _total++;

            T? obtenido;
            try
            {
                obtenido = obtenerValor();
            }
            catch (NotImplementedException)
            {
                ImprimirFalta(enunciado, "todavia no completaste este metodo (throw new NotImplementedException)");
                return;
            }
            catch (Exception ex)
            {
                ImprimirFalta(enunciado, $"tu codigo lanzo {ex.GetType().Name}: {ex.Message}");
                return;
            }

            var ok = SonIguales(esperado, obtenido);
            if (ok)
            {
                _correctos++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  [OK]    {enunciado}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [FALTA] {enunciado}");
                Console.WriteLine($"          esperaba: {Formatear(esperado)}");
                Console.WriteLine($"          tu codigo dio: {Formatear(obtenido)}");
                Console.ResetColor();
            }
        }

        // Sobrecarga para ejercicios async: "await" no se puede usar dentro
        // de un Func<T> comun sin volverlo Func<Task<T>>.
        public static async Task CheckAsync<T>(string enunciado, T esperado, Func<Task<T>> obtenerValorAsync)
        {
            _total++;

            T? obtenido;
            try
            {
                obtenido = await obtenerValorAsync();
            }
            catch (NotImplementedException)
            {
                ImprimirFalta(enunciado, "todavia no completaste este metodo (throw new NotImplementedException)");
                return;
            }
            catch (Exception ex)
            {
                ImprimirFalta(enunciado, $"tu codigo lanzo {ex.GetType().Name}: {ex.Message}");
                return;
            }

            var ok = SonIguales(esperado, obtenido);
            if (ok)
            {
                _correctos++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"  [OK]    {enunciado}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"  [FALTA] {enunciado}");
                Console.WriteLine($"          esperaba: {Formatear(esperado)}");
                Console.WriteLine($"          tu codigo dio: {Formatear(obtenido)}");
                Console.ResetColor();
            }
        }

        private static void ImprimirFalta(string enunciado, string motivo)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [FALTA] {enunciado}");
            Console.WriteLine($"          {motivo}");
            Console.ResetColor();
        }

        private static bool SonIguales<T>(T esperado, T obtenido)
        {
            // Las listas/arrays no se comparan bien con Equals (compara si
            // son el mismo objeto en memoria, no si tienen el mismo
            // contenido), asi que a las colecciones las comparamos elemento
            // por elemento. Los strings tambien implementan IEnumerable
            // (de chars) pero para esos si queremos la comparacion normal.
            if (esperado is IEnumerable secEsperada and not string &&
                obtenido is IEnumerable secObtenida and not string)
            {
                return secEsperada.Cast<object?>().SequenceEqual(secObtenida.Cast<object?>());
            }

            return Equals(esperado, obtenido);
        }

        private static string Formatear(object? valor)
        {
            if (valor is IEnumerable secuencia and not string)
            {
                return "[" + string.Join(", ", secuencia.Cast<object?>()) + "]";
            }

            return valor?.ToString() ?? "null";
        }

        public static void Resumen()
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"Resultado total: {_correctos}/{_total} ejercicios correctos.");
            if (_total > 0 && _correctos == _total)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Completaste todos los ejercicios de Fase 0. Estas list@ para la Fase 1.");
                Console.ResetColor();
            }
        }
    }
}
