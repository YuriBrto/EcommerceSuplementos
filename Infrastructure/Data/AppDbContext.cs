using Microsoft.EntityFrameworkCore;
using EcommerceSuplementos.Domain.Entity;

namespace EcommerceSuplementos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinho { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Carrinho ───────────────────────────────────────────
            modelBuilder.Entity<Carrinho>(b =>
            {
                b.HasKey(c => c.IdCarrinho);
                b.Property(c => c.CriadoEm).IsRequired();
                b.Property(c => c.AtualizadoEm).IsRequired();
                b.HasOne(c => c.Usuario)
                    .WithMany()
                    .HasForeignKey(c => c.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // ── ItemCarrinho ───────────────────────────────────────
            modelBuilder.Entity<ItemCarrinho>(b =>
            {
                b.HasKey(i => i.IdItemCarrinho);
                b.Property(i => i.Quantidade).IsRequired();
                b.HasOne(i => i.Carrinho)
                    .WithMany(c => c.Itens)
                    .HasForeignKey(i => i.IdCarrinho)
                    .OnDelete(DeleteBehavior.Cascade);

                // Restrict: impede deletar produto que está em um carrinho ativo
                b.HasOne(i => i.Produto)
                    .WithMany()
                    .HasForeignKey(i => i.IdProduto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ── Produto ───────────────────────────────────────────
            modelBuilder.Entity<Produto>(b =>
            {
                b.HasKey(p => p.IdProduto);
                b.Property(p => p.Preco).HasPrecision(18, 2).IsRequired();
            });

            // ── Pedido ────────────────────────────────────────────
            modelBuilder.Entity<Pedido>(b =>
            {
                b.HasKey(p => p.IdPedido);
                b.Property(p => p.Total).HasPrecision(18, 2).IsRequired();
            });

            // ── ItemPedido ────────────────────────────────────────
            modelBuilder.Entity<ItemPedido>(b =>
            {
                b.HasKey(i => i.IdItemPedido);
                b.Property(i => i.Preco).HasPrecision(18, 2).IsRequired();
            });

            // ── Avaliacao ─────────────────────────────────────────
            modelBuilder.Entity<Avaliacao>(b =>
            {
                b.HasKey(a => a.IdAvaliacao);
            });
        }
    }
}