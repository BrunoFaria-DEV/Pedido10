using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Npgsql;
using System.Data;

namespace Pedido10.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public async Task<bool> Add(Usuario usuario)
        {
            var path = Path.Combine("..", "Pedido10.Data", "Script", "Usuario", "AddUsuario.sql");
            
            var param = new NpgsqlParameter("@Nome", usuario.Nome);
            var param1 = new NpgsqlParameter("@Email", usuario.Email);
            var param2 = new NpgsqlParameter("@Senha", usuario.Senha);
            var param3 = new NpgsqlParameter("@Plano_Usuario", usuario.Plano_Usuario);
            var param4 = new NpgsqlParameter("@Status", usuario.Status);

            var listParams = new List<NpgsqlParameter>() { param, param1, param2, param3, param4 };

            var response = await Proxy.AnyQueryAsync(path, listParams);

            return response;
        }

        public async Task<Usuario> Find(int id)
        {
            var path = Path.Combine("..", "Pedido10.Data", "Script", "Usuario", "FindUsuario.sql");
            var param = new NpgsqlParameter("@ID_Usuario", id);
            var listParams = new List<NpgsqlParameter>() { param };

            var response = await Proxy.ReaderAsync(path, listParams);

            if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
            {
                var row = response.Tables[0].Rows[0];

                return new Usuario()
                {
                    ID_Usuario = row.Field<int>("ID_Usuario"),
                    Nome = row.Field<string>("Nome"),
                    Email = row.Field<string>("Email"),
                    Senha = row.Field<string>("Senha"),
                    Plano_Usuario = row.Field<char>("Plano_Usuario"),
                    Status = row.Field<char>("Status"),
                };
            }

            return null;
        }

        public async Task<Usuario> FindByEmail(string email)
        {
            var path = Path.Combine("..", "Pedido10.Data", "Script", "Usuario", "FindUsuarioByEmail.sql");
            var param = new NpgsqlParameter("@Email", email);
            var listParams = new List<NpgsqlParameter>() { param };

            var response = await Proxy.ReaderAsync(path, listParams);

            if (response.Tables.Count > 0 && response.Tables[0].Rows.Count > 0)
            {
                var row =  response.Tables[0].Rows[0];

                return new Usuario()
                {
                    ID_Usuario = row.Field<int>("ID_Usuario"),
                    Nome = row.Field<string>("Nome"),
                    Email = row.Field<string>("Email"),
                    Senha = row.Field<string>("Senha"),
                    Plano_Usuario = row.Field<char>("Plano_Usuario"),
                    Status = row.Field<char>("Status"),
                };
            }

            return null;
        }

        public Task<bool> Update(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
