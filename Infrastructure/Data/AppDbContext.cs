using Microsoft.EntityFrameworkCore;
using EcommerceSuplementos.Domain.Entity;
using System.Collections.Generic;

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
    }
}