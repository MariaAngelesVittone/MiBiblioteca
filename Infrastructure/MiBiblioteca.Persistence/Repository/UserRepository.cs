using Microsoft.EntityFrameworkCore;
using MiBiblioteca.Application.Interfaces.Repositories;
using MiBiblioteca.Domain.Entities;
using MiBiblioteca.Persistence.Context;

namespace MiBiblioteca.Persistence.Repository
{
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(MiBibliotecaContext context) : base(context) { }

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }
}
