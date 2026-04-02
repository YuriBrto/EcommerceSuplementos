using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario> CreateAsync(Usuario usuario);
        Task UpdateAsync(int id, Usuario usuario);
        Task DeleteAsync(int id);
    }
}
