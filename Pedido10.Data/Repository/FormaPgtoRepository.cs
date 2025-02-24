using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    public class FormaPgtoRepository : IFormaPgtoRepository
    {
        private readonly Pedido10Context _context;

        public FormaPgtoRepository(Pedido10Context context)
        {
            _context = context;
        }

        public async Task<List<Forma_PGTO>> GetAll() 
        {
            var formaPgtos = await _context.Forma_PGTO.ToListAsync();
 
            return formaPgtos;
        }

        public async Task<Forma_PGTO> Find(int id)
        {
            var formaPgto = await _context.Forma_PGTO.FindAsync(id);
            return formaPgto;
        }

        public async Task<Forma_PGTO> FindByName(string descFormaPgto)
        {
            var formaPgto = await _context.Forma_PGTO.SingleOrDefaultAsync(cliente => cliente.Desc_Forma_PGTO == descFormaPgto);
            return formaPgto;
        }

        public async Task<bool> Add(Forma_PGTO formaPgto)
        {
            try
            {
                _context.Forma_PGTO.Add(formaPgto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Forma_PGTO formaPgto)
        {
            try
            {
                _context.Forma_PGTO.Update(formaPgto);
                Console.WriteLine(formaPgto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Forma_PGTO formaPgto)
        {
            try
            {
                _context.Forma_PGTO.Remove(formaPgto);
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
