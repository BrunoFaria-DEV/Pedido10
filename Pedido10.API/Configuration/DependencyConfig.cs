﻿using FluentValidation;
using Pedido10.Application.Contract;
using Pedido10.Application.Contract.Auth;
using Pedido10.Application.Service;
using Pedido10.Application.Service.Auth;
using Pedido10.Application.Validator;
using Pedido10.Data.Contract;
using Pedido10.Data.Repository;
using Pedido10.Domain.Entity;
using System.Runtime.CompilerServices;

namespace Pedido10.API.Configuration
{
    public static class DependencyConfig
    {
        public static void RegisterConfig(this IServiceCollection service)
        {
            //service.AddScoped<CreateUsuarioValidator>();
            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IUsuarioService, UsuarioService>();
            service.AddScoped<IUsuarioRepository, UsuarioRepository>();
            service.AddScoped<IClienteService, ClienteService>();
            service.AddScoped<IClienteRepository, ClienteRepository>();
            service.AddScoped<IProdutoService, ProdutoService>();
            service.AddScoped<IProdutoRepository, ProdutoRepository>();
            service.AddScoped<ICidadeService, CidadeService>();
            service.AddScoped<ICidadeRepository, CidadeRepository>();
            service.AddScoped<IFormaPgtoService, FormaPgtoService>();
            service.AddScoped<IFormaPgtoRepository, FormaPgtoRepository>();
            service.AddScoped<PedidoService>();
            service.AddScoped<PedidoRepository>();
        }
    }
}
