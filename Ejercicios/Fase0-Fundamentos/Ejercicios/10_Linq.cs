namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 10: LINQ basico (Where, Select, FirstOrDefault, OrderBy)
    // ============================================================
    // LINQ permite hacer consultas sobre listas (y sobre tablas de una
    // base de datos, via Entity Framework) de forma declarativa: en vez de
    // escribir un foreach con ifs, describis QUE queres, no COMO
    // conseguirlo. Es literalmente lo que usan los repositorios de
    // MiBiblioteca: RepositoryBase.GetAsync(predicate) llama por dentro a
    // "_dbSet.FirstOrDefaultAsync(predicate)" - el mismo FirstOrDefault
    // que vas a usar aca, solo que sobre una lista en memoria en vez de
    // sobre la base de datos.
    //
    // Reutilizamos la clase Libro del Ejercicio 6.
    public static class Ej10Linq
    {
        // Debe devolver una nueva lista solo con los libros de un autor
        // dado (comparacion exacta). Pista: libros.Where(x => condicion)
        // devuelve una secuencia filtrada - convertila a lista con
        // .ToList().
        public static List<Libro> DeAutor(List<Libro> libros, string autor)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver una lista solo con los TITULOS (no los libros
        // completos) de los libros terminados (EstaTerminado() true).
        // Pista: encadena .Where(...) y .Select(...).
        public static List<string> TitulosDeLibrosTerminados(List<Libro> libros)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver el primer libro cuyo Titulo sea exactamente
        // "titulo", o null si no existe ninguno. Pista:
        // FirstOrDefault(x => condicion) devuelve el primer elemento que
        // cumple, o null (el valor por defecto de una clase) si ninguno
        // cumple.
        public static Libro? BuscarPorTitulo(List<Libro> libros, string titulo)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver los libros ordenados por PorcentajeLeido() de
        // mayor a menor. Pista:
        // libros.OrderByDescending(x => x.PorcentajeLeido()).ToList()
        public static List<Libro> OrdenarPorProgresoDescendente(List<Libro> libros)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
