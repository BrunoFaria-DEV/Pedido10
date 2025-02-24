using Microsoft.EntityFrameworkCore;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Context
{
    public class Pedido10Context : DbContext
    {
        public Pedido10Context(DbContextOptions<Pedido10Context> options) : base(options) { }

        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Produto> Produto => Set<Produto>();
        public DbSet<Forma_PGTO> Forma_PGTO => Set<Forma_PGTO>();
        public DbSet<Cidade> Cidade => Set<Cidade>();
        public DbSet<Pedido> Pedido => Set<Pedido>();
        public DbSet<Pedido_Produto> Pedido_Produto => Set<Pedido_Produto>();
        public DbSet<Parcela> Parcela => Set<Parcela>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Utils.StringConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido_Produto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.Pedido_Produtos)
                .HasForeignKey(pp => pp.ID_Pedido);

            modelBuilder.Entity<Pedido_Produto>()
                .HasOne(pp => pp.Produto)
                .WithMany()
                .HasForeignKey(pp => pp.ID_Produto);
        }

    }
}
