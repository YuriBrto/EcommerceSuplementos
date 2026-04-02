using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;

namespace EcommerceSuplementos.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo) => _repo = repo;

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Usuario?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var existente = await _repo.GetByEmailAsync(usuario.EmailUsuario);
            if (existente != null)
                throw new InvalidOperationException("E-mail já cadastrado.");

            // Aqui futuramente entra o hash da senha (BCrypt)
            await _repo.AddAsync(usuario);
            await _repo.SaveAsync();
            return usuario;
        }

        public async Task UpdateAsync(int id, Usuario usuario)
        {
            var existente = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Usuário não encontrado.");

            existente.NomeUsuario = usuario.NomeUsuario;
            existente.EmailUsuario = usuario.EmailUsuario;
            existente.Peso = usuario.Peso;
            existente.Altura = usuario.Altura;
            existente.Objetivo = usuario.Objetivo;

            await _repo.UpdateAsync(existente);
            await _repo.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Usuário não encontrado.");
            await _repo.DeleteAsync(usuario);
            await _repo.SaveAsync();
        }
    }
}
