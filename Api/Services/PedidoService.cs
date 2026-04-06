using EcommerceSuplementos.Api.DTOs;
using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Entity.DTOs;
using EcommerceSuplementos.Domain.Interfaces;

namespace EcommerceSuplementos.Api.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repo;
        public PedidoService(IPedidoRepository repo) => _repo = repo;

        public async Task<IEnumerable<PedidoResponse>> GetAllAsync()
        {
            var pedidos = await _repo.GetAllAsync();

            return pedidos.Select(p => new PedidoResponse
            {
                IdPedido = p.IdPedido,
                IdUsuario = p.IdUsuario,
                Total = p.Total,
                Status = p.Status,
                DataCriacao = p.DataCriacao,
                ItensPedido = p.ItensPedido?.Select(i => new ItemPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    Preco = i.Preco
                }).ToList()
            });
        }

        public async Task<PedidoResponse?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new PedidoResponse
            {
                IdPedido = p.IdPedido,
                IdUsuario = p.IdUsuario,
                Total = p.Total,
                Status = p.Status,
                DataCriacao = p.DataCriacao,
                ItensPedido = p.ItensPedido?.Select(i => new ItemPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    Preco = i.Preco
                }).ToList()
            };
        }

        public async Task<IEnumerable<PedidoResponse>> GetByUsuarioAsync(int idUsuario)
        {
            var pedidos = await _repo.GetByUsuarioAsync(idUsuario);

            return pedidos.Select(p => new PedidoResponse
            {
                IdPedido = p.IdPedido,
                IdUsuario = p.IdUsuario,
                Total = p.Total,
                Status = p.Status,
                DataCriacao = p.DataCriacao,
                ItensPedido = p.ItensPedido?.Select(i => new ItemPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    Preco = i.Preco
                }).ToList()
            });
        }

        public async Task<PedidoResponse> CreateAsync(CriarPedidoRequest request)
        {
            var pedido = new Pedido
            {
                IdUsuario = request.IdUsuario,
                Total = request.Total,
                Status = "Processando",
                DataCriacao = DateTime.UtcNow,
                ItensPedido = request.ItensPedido.Select(i => new ItemPedido
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    Preco = i.Preco
                }).ToList()
            };

            await _repo.AddAsync(pedido);
            await _repo.SaveAsync();

            return new PedidoResponse
            {
                IdPedido = pedido.IdPedido,
                IdUsuario = pedido.IdUsuario,
                Total = pedido.Total,
                Status = pedido.Status,
                DataCriacao = pedido.DataCriacao,
                ItensPedido = pedido.ItensPedido.Select(i => new ItemPedidoResponse
                {
                    IdProduto = i.IdProduto,
                    Quantidade = i.Quantidade,
                    Preco = i.Preco
                }).ToList()
            };
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var pedido = await _repo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Pedido não encontrado.");

            pedido.Status = status;

            await _repo.UpdateAsync(pedido);
            await _repo.SaveAsync();
        }
    }
}
