using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Pedido10Context _context;

        public ProdutoRepository(Pedido10Context context) 
        { 
            _context = context;
        }

        public async Task<List<Produto>> GetAll()
        {
            var produtos = await _context.Produto.OrderByDescending(produto => produto.ID_Produto).ToListAsync();
            return produtos;
        }

        public async Task<Produto> Find(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            return produto;
        }

        public async Task<Produto> FindByName(string nome)
        {
            var produto = await _context.Produto.SingleOrDefaultAsync(cliente => cliente.Nome_Produto == nome);
            return produto;
        }

        public async Task<bool> Add(Produto produto)
        {
            try
            {
                _context.Produto.Add(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Produto produto)
        {
            try
            {
                _context.Produto.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Produto produto)
        {
            try
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
