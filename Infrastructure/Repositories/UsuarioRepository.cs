using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceSuplementos.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Usuario>> GetAllAsync() =>
            await _context.Usuarios.ToListAsync();

        public async Task<Usuario?> GetByIdAsync(int id) =>
            await _context.Usuarios.FindAsync(id);

        public async Task<Usuario?> GetByEmailAsync(string email) =>
            await _context.Usuarios.FirstOrDefaultAsync(u => u.EmailUsuario == email);

        public async Task AddAsync(Usuario usuario) =>
            await _context.Usuarios.AddAsync(usuario);

        public async Task UpdateAsync(Usuario usuario) =>
            _context.Usuarios.Update(usuario);

        public async Task DeleteAsync(Usuario usuario) =>
            _context.Usuarios.Remove(usuario);

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}
