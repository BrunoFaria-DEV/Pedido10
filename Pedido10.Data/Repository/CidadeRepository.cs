﻿using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    internal class CidadeRepository : ICidadeRepository
    {
        private readonly Pedido10Context _context;
        public CidadeRepository(Pedido10Context context) 
        {
            _context = context;
        }

        public Task<Cidade> Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cidade>> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cidade>> FindByUf(string uf)
        {
            throw new NotImplementedException();
        }
    }
}
