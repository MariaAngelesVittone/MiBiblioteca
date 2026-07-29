namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 2: Condicionales (if / else / switch)
    // ============================================================
    // Un programa casi nunca hace siempre lo mismo: tiene que decidir que
    // hacer segun los datos. Para eso estan los condicionales:
    //
    //   if (condicion) { ... } else { ... }
    //
    // Se puede encadenar con "else if" para mas de dos casos, y "switch"
    // es una forma mas corta de escribir "si es A hace esto, si es B hace
    // esto otro..." cuando comparamos UNA variable contra varios valores
    // posibles.
    public static class Ej02Condicionales
    {
        // Debe devolver "aprobado" si la nota es mayor o igual a 6,
        // "desaprobado" en caso contrario.
        public static string EvaluarNota(int nota)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el mayor de los tres numeros.
        public static int Mayor(int a, int b, int c)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el nombre del dia de la semana a partir de su
        // numero (1 = "Lunes", 2 = "Martes", ..., 7 = "Domingo").
        // Si el numero no esta entre 1 y 7, debe devolver "Invalido".
        // Pista: esto es un buen lugar para usar switch.
        public static string NombreDelDia(int numeroDeDia)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el estado de una suscripcion segun cuantas
        // conversiones lleva usadas el mes, imitando la logica real de
        // MiBiblioteca/ConversorDeMoneda:
        //   - si "esPro" es true, siempre "ilimitado"
        //   - si no es pro y usadas < limite, "disponible"
        //   - si no es pro y usadas >= limite, "limite alcanzado"
        public static string EstadoSuscripcion(bool esPro, int usadas, int limite)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
