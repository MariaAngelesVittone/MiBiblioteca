using MiBiblioteca.Domain.Common;

namespace MiBiblioteca.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public List<ReadingEntry> ReadingEntries { get; set; } = new();
    }
}
