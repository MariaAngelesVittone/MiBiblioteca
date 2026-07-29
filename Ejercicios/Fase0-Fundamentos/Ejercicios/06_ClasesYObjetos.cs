namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 6: Clases y objetos (POO basica)
    // ============================================================
    // Una CLASE es un plano para crear objetos: describe que datos tiene
    // (propiedades) y que puede hacer (metodos). Un OBJETO es una
    // instancia concreta de esa clase - "Clean Code" seria un objeto de
    // la clase Libro, "Effective Java" otro objeto de la MISMA clase.
    //
    // Esto es exactamente lo que hace la entidad Book en MiBiblioteca
    // (Core/MiBiblioteca.Domain/Entities/Book.cs): una clase con
    // propiedades (Isbn, Title, Author...) que se instancia una vez por
    // cada libro real que existe en la base de datos.
    //
    // Las propiedades y el constructor de Libro ya estan escritos (si los
    // dejaramos incompletos, el proyecto entero no compilaria y no
    // podrias correr NINGUN ejercicio). Tu trabajo es completar los TRES
    // metodos de abajo.
    public class Libro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int PaginasLeidas { get; set; }
        public int TotalPaginas { get; set; }

        public Libro(string titulo, string autor, int totalPaginas)
        {
            Titulo = titulo;
            Autor = autor;
            TotalPaginas = totalPaginas;
            PaginasLeidas = 0;
        }

        // Debe sumar "paginas" a PaginasLeidas, sin pasarse de
        // TotalPaginas (si se pasa, PaginasLeidas queda en TotalPaginas).
        public void Leer(int paginas)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el porcentaje leido, de 0 a 100 (int, truncado).
        // Pista: (PaginasLeidas * 100) / TotalPaginas, pero cuidado con la
        // division entera - si PaginasLeidas y TotalPaginas son ambos int,
        // "PaginasLeidas / TotalPaginas" se trunca ANTES de multiplicar
        // por 100 y el resultado da siempre 0. Multiplica primero.
        public int PorcentajeLeido()
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver true si PaginasLeidas es igual a TotalPaginas.
        public bool EstaTerminado()
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }

    public static class Ej06ClasesYObjetos
    {
        // Ya completos: usan la clase Libro de arriba para que puedas
        // probar tu implementacion sin escribir el "new Libro(...)" cada
        // vez en el runner.
        public static int LeerYObtenerPaginasLeidas(int totalPaginas, int paginasALeer)
        {
            var libro = new Libro("Titulo de prueba", "Autor de prueba", totalPaginas);
            libro.Leer(paginasALeer);
            return libro.PaginasLeidas;
        }

        public static int LeerYObtenerPorcentaje(int totalPaginas, int paginasALeer)
        {
            var libro = new Libro("Titulo de prueba", "Autor de prueba", totalPaginas);
            libro.Leer(paginasALeer);
            return libro.PorcentajeLeido();
        }

        public static bool LeerYVerificarTerminado(int totalPaginas, int paginasALeer)
        {
            var libro = new Libro("Titulo de prueba", "Autor de prueba", totalPaginas);
            libro.Leer(paginasALeer);
            return libro.EstaTerminado();
        }
    }
}
