using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
//using System.Security.Cryptography; Reservado para estudos de criptografia de senhas
using System.Text;

namespace Pedido10.Application.Service
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<List<UsuarioDto>> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAll();

            List<UsuarioDto> usuariosDto = [];

            foreach (var usuario in usuarios) 
            {
                usuariosDto.Add(new UsuarioDto()
                {
                    ID_Usuario = usuario.ID_Usuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Plano_Usuario = usuario.Plano_Usuario,
                    Status = usuario.Status,
                });
            }

            return usuariosDto;
        }

        public async Task<bool> Add(UsuarioDto dto)
        {
            var retorno = false;

            var existeUsuario = await _usuarioRepository.FindByEmail(dto.Email);
            if (existeUsuario != null) throw new Exception($"{dto.Email} já existe!");

            // Reservado para estudos posteriores de criptografia de senhas 
            //using (var hmac = new HMACSHA512())
            //{
            //    var senhaSal = hmac.Key;
            //    var senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Senha));
            //    var usuario = new Usuario(dto.NomeCompleto, senhaHash, senhaSal, dto.Email, dto.Telefone);

            //    retorno = await _usuarioRepository.Add(usuario);
            //}

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dto.Senha);
            string senhaBase64 = Convert.ToBase64String(bytes);
            var usuario = new Usuario(dto.Nome, dto.Email, senhaBase64, dto.Plano_Usuario, dto.Status);

            retorno = await _usuarioRepository.Add(usuario);

            return retorno;
        }

        public Task<UsuarioDto> Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int usuarioId, UsuarioDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
