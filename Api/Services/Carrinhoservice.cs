using EcommerceSuplementos.Domain.DTOs;
using EcommerceSuplementos.Domain.Entity;
using EcommerceSuplementos.Domain.Interfaces;
using EcommerceSuplementos.Domain.Interfaces.Repositories;

namespace EcommerceSuplementos.Api.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _repo;
        private readonly IProdutoRepository _produtoRepo;

        public CarrinhoService(ICarrinhoRepository repo, IProdutoRepository produtoRepo)
        {
            _repo = repo;
            _produtoRepo = produtoRepo;
        }

        // ── GET ────────────────────────────────────────────────
        public async Task<CarrinhoResponseDto> GetCarrinhoAsync(int idUsuario)
        {
            var carrinho = await ObterOuCriarCarrinhoAsync(idUsuario);
            return MapToDto(carrinho);
        }

        // ── ADD ITEM ───────────────────────────────────────────
        public async Task<CarrinhoResponseDto> AddItemAsync(int idUsuario, ItemCarrinhoRequestDto dto)
        {
            if (dto.Quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            var produto = await _produtoRepo.GetByIdAsync(dto.IdProduto)
                ?? throw new KeyNotFoundException("Produto não encontrado.");

            if (produto.Estoque < dto.Quantidade)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {produto.Estoque}.");

            var carrinho = await ObterOuCriarCarrinhoAsync(idUsuario);

            // Se o item já existe, incrementa a quantidade
            var itemExistente = await _repo.GetItemAsync(carrinho.IdCarrinho, dto.IdProduto);
            if (itemExistente != null)
            {
                var novaQtd = itemExistente.Quantidade + dto.Quantidade;
                if (novaQtd > produto.Estoque)
                    throw new InvalidOperationException($"Estoque insuficiente. Disponível: {produto.Estoque}.");

                itemExistente.Quantidade = novaQtd;
                await _repo.UpdateItemAsync(itemExistente);
            }
            else
            {
                var novoItem = new ItemCarrinho
                {
                    IdCarrinho = carrinho.IdCarrinho,
                    IdProduto = dto.IdProduto,
                    Quantidade = dto.Quantidade,
                };
                await _repo.AddItemAsync(novoItem);
            }

            carrinho.AtualizadoEm = DateTime.UtcNow;
            await _repo.SaveAsync();

            // Recarrega para retornar dados atualizados com navegação
            var atualizado = await _repo.GetByUsuarioAsync(idUsuario);
            return MapToDto(atualizado!);
        }

        // ── UPDATE ITEM ────────────────────────────────────────
        public async Task<CarrinhoResponseDto> UpdateItemAsync(int idUsuario, int idItemCarrinho, ItemCarrinhoRequestDto dto)
        {
            if (dto.Quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero.");

            var carrinho = await _repo.GetByUsuarioAsync(idUsuario)
                ?? throw new KeyNotFoundException("Carrinho não encontrado.");

            var item = await _repo.GetItemByIdAsync(idItemCarrinho)
                ?? throw new KeyNotFoundException("Item não encontrado no carrinho.");

            if (item.IdCarrinho != carrinho.IdCarrinho)
                throw new UnauthorizedAccessException("Item não pertence ao carrinho do usuário.");

            var produto = await _produtoRepo.GetByIdAsync(item.IdProduto)
                ?? throw new KeyNotFoundException("Produto não encontrado.");

            if (dto.Quantidade > produto.Estoque)
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {produto.Estoque}.");

            item.Quantidade = dto.Quantidade;
            carrinho.AtualizadoEm = DateTime.UtcNow;

            await _repo.UpdateItemAsync(item);
            await _repo.SaveAsync();

            var atualizado = await _repo.GetByUsuarioAsync(idUsuario);
            return MapToDto(atualizado!);
        }

        // ── REMOVE ITEM ────────────────────────────────────────
        public async Task<CarrinhoResponseDto> RemoveItemAsync(int idUsuario, int idItemCarrinho)
        {
            var carrinho = await _repo.GetByUsuarioAsync(idUsuario)
                ?? throw new KeyNotFoundException("Carrinho não encontrado.");

            var item = await _repo.GetItemByIdAsync(idItemCarrinho)
                ?? throw new KeyNotFoundException("Item não encontrado.");

            if (item.IdCarrinho != carrinho.IdCarrinho)
                throw new UnauthorizedAccessException("Item não pertence ao carrinho do usuário.");

            carrinho.AtualizadoEm = DateTime.UtcNow;

            await _repo.DeleteItemAsync(item);
            await _repo.SaveAsync();

            var atualizado = await _repo.GetByUsuarioAsync(idUsuario);
            return MapToDto(atualizado!);
        }

        // ── CLEAR ──────────────────────────────────────────────
        public async Task ClearAsync(int idUsuario)
        {
            var carrinho = await _repo.GetByUsuarioAsync(idUsuario);
            if (carrinho == null) return;

            foreach (var item in carrinho.Itens.ToList())
                await _repo.DeleteItemAsync(item);

            carrinho.AtualizadoEm = DateTime.UtcNow;
            await _repo.SaveAsync();
        }

        // ── HELPERS ────────────────────────────────────────────

        /// <summary>Busca o carrinho do usuário ou cria um novo se não existir.</summary>
        private async Task<Carrinho> ObterOuCriarCarrinhoAsync(int idUsuario)
        {
            var carrinho = await _repo.GetByUsuarioAsync(idUsuario);
            if (carrinho != null) return carrinho;

            var novo = new Carrinho { IdUsuario = idUsuario };
            await _repo.AddAsync(novo);
            await _repo.SaveAsync();

            return (await _repo.GetByUsuarioAsync(idUsuario))!;
        }

        private static CarrinhoResponseDto MapToDto(Carrinho carrinho) => new()
        {
            IdCarrinho = carrinho.IdCarrinho,
            IdUsuario = carrinho.IdUsuario,
            Itens = carrinho.Itens.Select(i => new ItemCarrinhoResponseDto
            {
                IdItemCarrinho = i.IdItemCarrinho,
                IdProduto = i.IdProduto,
                Nome = i.Produto?.Nome ?? string.Empty,
                Marca = i.Produto?.Marca ?? string.Empty,
                ImagemUrl = i.Produto?.ImagemUrl,
                PrecoUnitario = i.Produto?.Preco ?? 0,
                Quantidade = i.Quantidade,
            }).ToList(),
        };
    }
}