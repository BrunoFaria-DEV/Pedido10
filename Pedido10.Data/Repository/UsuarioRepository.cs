using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Npgsql;
using System.Data;
using Pedido10.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Pedido10.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Pedido10Context _context;

        public UsuarioRepository(Pedido10Context context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAll()
        {
            var usuarios = await _context.Usuario.OrderByDescending(usuario => usuario.ID_Usuario).ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> Find(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            return usuario;
        }

        public async Task<Usuario> FindByEmail(string email)
        {
            var Usuario = await _context.Usuario.SingleOrDefaultAsync(cliente => cliente.Email == email);
            return Usuario;
        }

        public async Task<bool> Add(Usuario usuario)
        {
            try
            {
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update(Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete(Usuario usuario)
        {
            try
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Usuario.AnyAsync(usuario => usuario.Email == email);
        }

        public async Task<bool> UpdateEmailExists(string email, int id)
        {
            return await _context.Usuario.AnyAsync(usuario => usuario.Email == email && usuario.ID_Usuario != id);
        }
    }
}
