using Fase0Fundamentos;
using Fase0Fundamentos.Ejercicios;

// Este archivo corre todos los ejercicios y te dice cuales te faltan.
// No hace falta que lo entiendas para aprender C# - anda directo a la
// carpeta Ejercicios/ y arranca por 01_Variables.cs. Despues de editar un
// archivo, guardalo y volve a correr "dotnet run" para ver si lo lograste.

Console.WriteLine("Fase 0: Fundamentos de programacion y C#");
Console.WriteLine("Completa los TODO en Ejercicios/ y volve a correr 'dotnet run'.");

Verificador.Titulo("Ejercicio 1: Variables y operadores");
Verificador.Check("Sumar(2, 3) debe dar 5", 5, () => Ej01Variables.Sumar(2, 3));
Verificador.Check("AreaRectangulo(4, 5) debe dar 20", 20d, () => Ej01Variables.AreaRectangulo(4, 5));
Verificador.Check("Saludar(\"Ana\") debe dar 'Hola, Ana!'", "Hola, Ana!", () => Ej01Variables.Saludar("Ana"));
Verificador.Check("EsPar(4) debe dar true", true, () => Ej01Variables.EsPar(4));
Verificador.Check("EsPar(7) debe dar false", false, () => Ej01Variables.EsPar(7));
Verificador.Check("PuedeVotar(16) debe dar true", true, () => Ej01Variables.PuedeVotar(16));
Verificador.Check("PuedeVotar(15) debe dar false", false, () => Ej01Variables.PuedeVotar(15));

Verificador.Titulo("Ejercicio 2: Condicionales");
Verificador.Check("EvaluarNota(6) debe dar 'aprobado'", "aprobado", () => Ej02Condicionales.EvaluarNota(6));
Verificador.Check("EvaluarNota(5) debe dar 'desaprobado'", "desaprobado", () => Ej02Condicionales.EvaluarNota(5));
Verificador.Check("Mayor(3, 7, 5) debe dar 7", 7, () => Ej02Condicionales.Mayor(3, 7, 5));
Verificador.Check("NombreDelDia(1) debe dar 'Lunes'", "Lunes", () => Ej02Condicionales.NombreDelDia(1));
Verificador.Check("NombreDelDia(7) debe dar 'Domingo'", "Domingo", () => Ej02Condicionales.NombreDelDia(7));
Verificador.Check("NombreDelDia(9) debe dar 'Invalido'", "Invalido", () => Ej02Condicionales.NombreDelDia(9));
Verificador.Check("EstadoSuscripcion(pro) debe dar 'ilimitado'", "ilimitado", () => Ej02Condicionales.EstadoSuscripcion(true, 999, 100));
Verificador.Check("EstadoSuscripcion(free, dentro del limite) debe dar 'disponible'", "disponible", () => Ej02Condicionales.EstadoSuscripcion(false, 50, 100));
Verificador.Check("EstadoSuscripcion(free, en el limite) debe dar 'limite alcanzado'", "limite alcanzado", () => Ej02Condicionales.EstadoSuscripcion(false, 100, 100));

Verificador.Titulo("Ejercicio 3: Bucles");
Verificador.Check("SumaHasta(5) debe dar 15", 15, () => Ej03Bucles.SumaHasta(5));
Verificador.Check("Factorial(4) debe dar 24", 24L, () => Ej03Bucles.Factorial(4));
Verificador.Check("Factorial(0) debe dar 1", 1L, () => Ej03Bucles.Factorial(0));
Verificador.Check("ContarPares([1,2,3,4,5,6]) debe dar 3", 3, () => Ej03Bucles.ContarPares(new List<int> { 1, 2, 3, 4, 5, 6 }));
Verificador.Check("Maximo([3,9,2,7]) debe dar 9", 9, () => Ej03Bucles.Maximo(new List<int> { 3, 9, 2, 7 }));

Verificador.Titulo("Ejercicio 4: Metodos");
Verificador.Check("PrimosHasta(10) debe dar [2,3,5,7]", new List<int> { 2, 3, 5, 7 }, () => Ej04Metodos.PrimosHasta(10));
Verificador.Check("ContarPrimosHasta(10) debe dar 4", 4, () => Ej04Metodos.ContarPrimosHasta(10));
Verificador.Check("PrecioConDescuento(100, 20) debe dar 80", 80d, () => Ej04Metodos.PrecioConDescuento(100, 20));
Verificador.Check("PrecioConDobleDescuento(100, 20, 10) debe dar 72", 72d, () => Ej04Metodos.PrecioConDobleDescuento(100, 20, 10));

Verificador.Titulo("Ejercicio 5: Colecciones");
Verificador.Check("DuplicarCadaUno([1,2,3]) debe dar [2,4,6]", new List<int> { 2, 4, 6 }, () => Ej05ColeccionesYArrays.DuplicarCadaUno(new List<int> { 1, 2, 3 }));
Verificador.Check("FiltrarMayoresA([1,5,10,15], 6) debe dar [10,15]", new List<int> { 10, 15 }, () => Ej05ColeccionesYArrays.FiltrarMayoresA(new List<int> { 1, 5, 10, 15 }, 6));

var stock = new Dictionary<string, int> { ["Dune"] = 3, ["1984"] = 0, ["Emma"] = 0 };
Verificador.Check("HayStock(stock, \"Dune\") debe dar true", true, () => Ej05ColeccionesYArrays.HayStock(stock, "Dune"));
Verificador.Check("HayStock(stock, \"1984\") debe dar false (cantidad 0)", false, () => Ej05ColeccionesYArrays.HayStock(stock, "1984"));
Verificador.Check("HayStock(stock, \"Nope\") debe dar false (no existe)", false, () => Ej05ColeccionesYArrays.HayStock(stock, "Nope"));
Verificador.Check("TitulosSinStock(stock) debe dar [\"1984\",\"Emma\"]", new List<string> { "1984", "Emma" }, () => Ej05ColeccionesYArrays.TitulosSinStock(stock));

Verificador.Titulo("Ejercicio 6: Clases y objetos");
Verificador.Check("Leer 50 de 300 paginas deja PaginasLeidas en 50", 50, () => Ej06ClasesYObjetos.LeerYObtenerPaginasLeidas(300, 50));
Verificador.Check("Leer 350 de 300 paginas no se pasa de 300 (tope)", 300, () => Ej06ClasesYObjetos.LeerYObtenerPaginasLeidas(300, 350));
Verificador.Check("Leer 100 de 200 paginas da 50% leido", 50, () => Ej06ClasesYObjetos.LeerYObtenerPorcentaje(200, 100));
Verificador.Check("Leer 50 de 200 paginas da 25% leido", 25, () => Ej06ClasesYObjetos.LeerYObtenerPorcentaje(200, 50));
Verificador.Check("Leer 100 de 100 paginas queda terminado", true, () => Ej06ClasesYObjetos.LeerYVerificarTerminado(100, 100));
Verificador.Check("Leer 50 de 100 paginas NO queda terminado", false, () => Ej06ClasesYObjetos.LeerYVerificarTerminado(100, 50));

Verificador.Titulo("Ejercicio 7: Herencia e interfaces");
Verificador.Check("DescripcionDePrestamo de un LibroFisico", "Libro fisico 'Dune' entregado en mano.", () => Ej07HerenciaEInterfaces.DescribirPrestamo(new LibroFisico("Dune")));
Verificador.Check("DescripcionDePrestamo de un Ebook", "Ebook 'Dune' enviado por link de descarga.", () => Ej07HerenciaEInterfaces.DescribirPrestamo(new Ebook("Dune")));

var materialParaPrestar = new LibroFisico("Ejemplar unico");
Verificador.Check("Primer intento de prestamo da true", true, () => Ej07HerenciaEInterfaces.IntentarPrestar(materialParaPrestar));
Verificador.Check("Segundo intento (ya prestado) da false", false, () => Ej07HerenciaEInterfaces.IntentarPrestar(materialParaPrestar));

Verificador.Titulo("Ejercicio 8: Excepciones");
Verificador.Check("DividirSeguro(10, 2) debe dar 5", 5d, () => Ej08Excepciones.DividirSeguro(10, 2));
Verificador.Check("DividirSeguro(10, 0) lanza DivideByZeroException con el mensaje correcto", true, () =>
{
    try
    {
        Ej08Excepciones.DividirSeguro(10, 0);
        return false;
    }
    catch (DivideByZeroException ex)
    {
        return ex.Message == "No se puede dividir por cero.";
    }
});
Verificador.Check("ValidarEdad(30) debe dar 30", 30, () => Ej08Excepciones.ValidarEdad(30));
Verificador.Check("ValidarEdad(-1) lanza ArgumentOutOfRangeException", true, () =>
{
    try { Ej08Excepciones.ValidarEdad(-1); return false; }
    catch (ArgumentOutOfRangeException) { return true; }
});
Verificador.Check("ValidarEdad(150) lanza ArgumentOutOfRangeException", true, () =>
{
    try { Ej08Excepciones.ValidarEdad(150); return false; }
    catch (ArgumentOutOfRangeException) { return true; }
});
Verificador.Check("EjecutarConValorPorDefecto sin error devuelve el resultado real", 5d, () => Ej08Excepciones.EjecutarConValorPorDefecto(() => 10.0 / 2, -1));
Verificador.Check("EjecutarConValorPorDefecto con error devuelve el valor por defecto", -1d, () => Ej08Excepciones.EjecutarConValorPorDefecto(() => throw new InvalidOperationException("boom"), -1));

Verificador.Titulo("Ejercicio 9: async/await");
await Verificador.CheckAsync("DuplicarLuegoDeEsperar(5, 10) debe dar 10", 10, () => Ej09AsyncAwait.DuplicarLuegoDeEsperar(5, 10));
await Verificador.CheckAsync("ConsultarPrecioConDescuentoAsync(100, 20) debe dar 80", 80d, () => Ej09AsyncAwait.ConsultarPrecioConDescuentoAsync(100, 20));
await Verificador.CheckAsync("SumarDosOperacionesEnParaleloAsync(3, 4) debe dar 14", 14, () => Ej09AsyncAwait.SumarDosOperacionesEnParaleloAsync(3, 4));

Verificador.Titulo("Ejercicio 10: LINQ");

// Se arma de nuevo en cada Check (en vez de una sola vez arriba) porque
// Leer() depende del Ejercicio 6: si todavia no lo completaste, queremos
// que ESTE ejercicio se marque como pendiente, no que se corte toda la
// ejecucion del programa con una excepcion sin atrapar.
List<Libro> CrearBiblioteca()
{
    var dune = new Libro("Dune", "Frank Herbert", 400);
    dune.Leer(400);
    var mil984 = new Libro("1984", "George Orwell", 300);
    mil984.Leer(150);
    var fundacion = new Libro("Fundacion", "Frank Herbert", 250);
    fundacion.Leer(250);
    return new List<Libro> { dune, mil984, fundacion };
}

Verificador.Check("DeAutor(biblioteca, \"Frank Herbert\") devuelve Dune y Fundacion", new List<string> { "Dune", "Fundacion" }, () => Ej10Linq.DeAutor(CrearBiblioteca(), "Frank Herbert").Select(l => l.Titulo).ToList());
Verificador.Check("TitulosDeLibrosTerminados(biblioteca) devuelve Dune y Fundacion", new List<string> { "Dune", "Fundacion" }, () => Ej10Linq.TitulosDeLibrosTerminados(CrearBiblioteca()));
Verificador.Check("BuscarPorTitulo(biblioteca, \"1984\") lo encuentra", "1984", () => Ej10Linq.BuscarPorTitulo(CrearBiblioteca(), "1984")?.Titulo ?? "NO ENCONTRADO");
Verificador.Check("BuscarPorTitulo(biblioteca, \"Nope\") no encuentra nada", "NO ENCONTRADO", () => Ej10Linq.BuscarPorTitulo(CrearBiblioteca(), "Nope")?.Titulo ?? "NO ENCONTRADO");
Verificador.Check("OrdenarPorProgresoDescendente ordena de mas leido a menos leido", new List<int> { 100, 100, 50 }, () => Ej10Linq.OrdenarPorProgresoDescendente(CrearBiblioteca()).Select(l => l.PorcentajeLeido()).ToList());

Verificador.Resumen();
