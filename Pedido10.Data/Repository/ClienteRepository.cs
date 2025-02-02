using Microsoft.EntityFrameworkCore;
using Pedido10.Data.Context;
using Pedido10.Data.Contract;
using Pedido10.Domain.Entity;

namespace Pedido10.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Pedido10Context _context;

        public ClienteRepository(Pedido10Context context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAll()
        {
            var clientes = await _context.Cliente.ToListAsync();

            return clientes;
        }

        public async Task<Cliente> Find(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            return cliente;
        }

        public async Task<Cliente> FindByEmail(string email)
        {
            var cliente = await _context.Cliente.SingleOrDefaultAsync(Cliente => Cliente.Email == email);

            return cliente;
        }

        public async Task<bool> Add(Cliente cliente)
        {
            try
            {
                var newCliente = await _context.Cliente.AddAsync(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Cliente cliente)
        {
            try
            {
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Cliente cliente)
        {
            try
            {
                _context.Cliente.Remove(cliente);
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
