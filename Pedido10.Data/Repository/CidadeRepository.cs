using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly Pedido10Context _context;
        public CidadeRepository(Pedido10Context context) 
        {
            _context = context;
        }

        public async Task<List<Cidade>> GetAll()
        {
            return await _context.Cidade.OrderBy(cidade => cidade.UF).ToListAsync();
        }

        public async Task<Cidade> Find(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            return cidade;
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
