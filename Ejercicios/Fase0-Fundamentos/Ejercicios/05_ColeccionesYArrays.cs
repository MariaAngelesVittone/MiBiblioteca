namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 5: Colecciones (List<T>, Dictionary<TKey, TValue>)
    // ============================================================
    // Hasta aca ya usamos List<int> sin explicarla: es una lista que puede
    // crecer o achicarse (a diferencia de un array, que tiene tamaño fijo).
    // El "<T>" es un GENERICO: significa "una lista DE ALGO", y ese algo se
    // especifica entre los <>. List<int> es una lista de enteros,
    // List<string> una lista de texto, List<Book> (en MiBiblioteca) una
    // lista de libros. Este es el mismo concepto que despues vas a ver en
    // IRepositoryBase<TEntity, TId> en la Fase 2 - alli el generico es la
    // ENTIDAD sobre la que trabaja el repositorio, no el contenido de una
    // lista, pero la idea de "parametrizar el tipo" es identica.
    //
    // Dictionary<TKey, TValue> guarda pares clave-valor, como una agenda
    // telefonica: la clave es el nombre, el valor es el numero. Buscar por
    // clave es mucho mas rapido que recorrer una lista buscando un dato.
    public static class Ej05ColeccionesYArrays
    {
        // Debe devolver una nueva lista con cada numero de "numeros"
        // multiplicado por 2. Pista: List<int> resultado = new(); despues
        // recorré "numeros" con foreach y agregá con resultado.Add(...).
        public static List<int> DuplicarCadaUno(List<int> numeros)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver una nueva lista solo con los numeros mayores a
        // "minimo" (sin modificar la lista original).
        public static List<int> FiltrarMayoresA(List<int> numeros, int minimo)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Recibe un diccionario que representa el stock de una libreria
        // (titulo -> cantidad disponible) y debe devolver true si el
        // titulo existe en el diccionario Y tiene stock mayor a 0.
        // Pista: Dictionary tiene un metodo TryGetValue(clave, out valor)
        // que devuelve true/false y te da el valor si existe.
        public static bool HayStock(Dictionary<string, int> stockPorTitulo, string titulo)
        {
            // TODO: completar
            throw new NotImplementedException();
        }

        // Debe devolver una lista con los titulos (las claves) que NO
        // tienen stock (cantidad igual a 0), en cualquier orden.
        public static List<string> TitulosSinStock(Dictionary<string, int> stockPorTitulo)
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }
}
