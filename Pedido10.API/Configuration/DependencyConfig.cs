using Pedido10.Application.Contract;
using Pedido10.Application.Contract.Auth;
using Pedido10.Application.Service;
using Pedido10.Application.Service.Auth;
using Pedido10.Data.Contract;
using Pedido10.Data.Repository;
using System.Runtime.CompilerServices;

namespace Pedido10.API.Configuration
{
    public static class DependencyConfig
    {
        public static void RegisterConfig(this IServiceCollection service)
        {
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IClienteService, ClienteService>();
            service.AddScoped<IClienteRepository, ClienteRepository>();
            service.AddScoped<IProdutoService, ProdutoService>();
            service.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
