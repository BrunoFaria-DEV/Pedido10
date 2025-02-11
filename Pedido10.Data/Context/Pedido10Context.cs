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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Utils.StringConnection);
        }

    }
}
