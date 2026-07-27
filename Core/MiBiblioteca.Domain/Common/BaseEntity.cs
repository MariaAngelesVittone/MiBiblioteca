namespace MiBiblioteca.Domain.Common
{
    // Todas las entidades del dominio heredan de aca. Guardamos cuando se creo/actualizo
    // un registro y usamos borrado logico (IsDeleted) en vez de borrar filas de verdad,
    // tal como se explica en el apunte de Clean Architecture.
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DateUpdated { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
