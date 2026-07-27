using MiBiblioteca.Domain.Entities;

namespace MiBiblioteca.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User, Guid>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
