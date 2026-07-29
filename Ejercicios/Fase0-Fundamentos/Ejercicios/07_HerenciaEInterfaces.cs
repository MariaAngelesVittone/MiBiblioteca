namespace Fase0Fundamentos.Ejercicios
{
    // ============================================================
    // EJERCICIO 7: Herencia e interfaces
    // ============================================================
    // Una INTERFAZ define un CONTRATO: dice que metodos/propiedades tiene
    // que tener cualquier clase que la implemente, pero no dice como estan
    // hechos por dentro. Esto es lo que ya usaste sin notarlo en toda
    // MiBiblioteca: IRepositoryBase, IBookService, IJwtTokenGenerator son
    // interfaces - Application solo conoce el contrato (que metodos hay),
    // nunca la implementacion concreta (que vive en Infrastructure).
    //
    // La HERENCIA es distinta: una clase hija hereda los datos y
    // comportamiento de una clase padre (con "class Hija : Padre"), y
    // puede agregar o redefinir cosas propias.
    public interface IPrestable
    {
        bool EstaDisponible { get; }
        void Prestar();
        void Devolver();
    }

    // Clase base con lo que comparten todos los materiales de biblioteca.
    // Ya esta completa: no hace falta tocarla.
    public abstract class MaterialDeBiblioteca : IPrestable
    {
        public string Titulo { get; }
        public bool EstaDisponible { get; protected set; } = true;

        protected MaterialDeBiblioteca(string titulo)
        {
            Titulo = titulo;
        }

        public void Prestar()
        {
            if (!EstaDisponible)
            {
                throw new InvalidOperationException($"'{Titulo}' ya esta prestado.");
            }
            EstaDisponible = false;
        }

        public void Devolver()
        {
            EstaDisponible = true;
        }

        // Marcarlo "abstract" OBLIGA a cada clase hija a dar su propia
        // implementacion - no hay una version por defecto, a proposito:
        // un libro fisico se entrega distinto que un ebook.
        public abstract string DescripcionDePrestamo();
    }

    public class LibroFisico : MaterialDeBiblioteca
    {
        public LibroFisico(string titulo) : base(titulo)
        {
        }

        // Debe devolver exactamente: "Libro fisico '<Titulo>' entregado en mano."
        public override string DescripcionDePrestamo()
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }

    public class Ebook : MaterialDeBiblioteca
    {
        public Ebook(string titulo) : base(titulo)
        {
        }

        // Debe devolver exactamente: "Ebook '<Titulo>' enviado por link de descarga."
        public override string DescripcionDePrestamo()
        {
            // TODO: completar
            throw new NotImplementedException();
        }
    }

    public static class Ej07HerenciaEInterfaces
    {
        // Ya completo.
        public static string DescribirPrestamo(MaterialDeBiblioteca material)
        {
            return material.DescripcionDePrestamo();
        }

        // Debe devolver false si "material" ya esta prestado
        // (EstaDisponible es false), true si lo pudo prestar. Esto es
        // POLIMORFISMO: el mismo codigo sirve para LibroFisico, Ebook, o
        // cualquier clase futura que herede de MaterialDeBiblioteca, sin
        // agregar un "if" por cada tipo nuevo.
        public static bool IntentarPrestar(MaterialDeBiblioteca material)
        {
            // TODO: completar
            // Pista: si material.EstaDisponible es true, llama a
            // material.Prestar() y devolvé true. Si no, devolvé false SIN
            // llamar a Prestar() (para no disparar la excepcion).
            throw new NotImplementedException();
        }
    }
}
