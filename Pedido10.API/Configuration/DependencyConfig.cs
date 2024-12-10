using Pedido10.Application.Contract;
using Pedido10.Application.Service;
using Pedido10.Data.Contract;
using Pedido10.Data.Repository;
using System.Runtime.CompilerServices;

namespace Pedido10.API.Configuration
{
    public static class DependencyConfig
    {
        public static void RegisterConfig(this IServiceCollection service)
        {
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IUsuarioService, UsuarioService>();
        }
    }
}
