using Microsoft.EntityFrameworkCore;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Context
{
    public class Pedido10Context : DbContext
    {

        public Pedido10Context(DbContextOptions<Pedido10Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Utils.StringConnection);
        }


        //public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Cliente> Cliente => Set<Cliente>();

    }
}
