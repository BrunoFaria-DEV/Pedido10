using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Data.Repository;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Pedido10.Shared.Results.Repository;
using System.Text;

namespace Pedido10.Application.Service
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<UsuarioDto?> Login(UsuarioDto usuarioDto)
        {
            var usuario = await _usuarioRepository.FindByEmail(usuarioDto.Email);
            if (usuario == null) return null;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(usuarioDto.Senha);
            string senhaBase64 = Convert.ToBase64String(bytes);
            if (usuario.Senha == senhaBase64)
            {
                return new UsuarioDto()
                {
                    ID_Usuario = usuario.ID_Usuario,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                };
            }
            return null;
        }

        public async Task<List<UsuarioDto>> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAll();
            if (usuarios.Count < 1 || !usuarios.Any())
            {
                return null;
            }

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

        public async Task<OperationResultGeneric<UsuarioDto>> Find(int id)
        {
            var usuario = await _usuarioRepository.Find(id);
            if (usuario == null)
            {
                return new OperationResultGeneric<UsuarioDto> { Success = false, Message = "Usuario não encontrado" };
            }

            UsuarioDto dto = new UsuarioDto()
            {
                ID_Usuario = usuario.ID_Usuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Plano_Usuario = usuario.Plano_Usuario,
                Status = usuario.Status,
                Tipo_Usuario = usuario.Tipo_Usuario,
            };

            return new OperationResultGeneric<UsuarioDto> { Success = true, Message = "Usuario encontrado.", Result = dto };
        }

        public async Task<OperationResultGeneric<UsuarioDto>> Add(UsuarioDto dto)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dto.Senha);
            string senhaBase64 = Convert.ToBase64String(bytes);
            var usuario = new Usuario(
                dto.Nome, 
                dto.Email, 
                senhaBase64, 
                dto.Plano_Usuario, 
                dto.Status
            );

            var resultado = await _usuarioRepository.Add(usuario);
            if (resultado != true)
            {
                return new OperationResultGeneric<UsuarioDto> { Success = false, Message = "Não foi Possivel incluir o Usuario." };
            }

            return new OperationResultGeneric<UsuarioDto> { Success = true, Message = "Usuario incluido com sucesso.", Result = dto };
        }

        public async Task<OperationResultGeneric<UsuarioDto>> Update(int usuarioId, UsuarioDto dto)
        {
            Usuario usuario = await _usuarioRepository.Find(usuarioId);
            if (usuario == null)
            {
                return new OperationResultGeneric<UsuarioDto> { Success = false, Message = "Não foi Possivel atualizar o Usuario pois não foi encontrado." };
            }

            usuario.Nome = dto.Nome ?? usuario.Nome;
            usuario.Email = dto.Email ?? usuario.Email;
            usuario.Status = dto.Status ?? usuario.Status;
            if (!String.IsNullOrEmpty(dto.Senha))
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dto.Senha);
                string senhaBase64 = Convert.ToBase64String(bytes);
                usuario.Senha = senhaBase64;
            }

            var resultado = await _usuarioRepository.Update(usuario);
            if (resultado != true)
            {
                return new OperationResultGeneric<UsuarioDto> { Success = false, Message = "Não foi Possivel atualizar o Usuario." };
            }

            return new OperationResultGeneric<UsuarioDto> { Success = true, Message = "Usuario atualizado com sucesso.", Result = dto };
        }

        public async Task<OperationResultGeneric<UsuarioDto>> Delete(int id)
        {
            Usuario usuario = await _usuarioRepository.Find(id);
            if (usuario == null)
            {
                return new OperationResultGeneric<UsuarioDto> { Success = false, Message = "Não foi Possivel excluir o Usuario pois não foi encontrado." };
            }

            var resultado = await _usuarioRepository.Delete(usuario);
            if (resultado != true)
            {
                return new OperationResultGeneric<UsuarioDto> { Success = false, Message = "Não foi Possivel excluir o Usuario." };
            }

            string nomeUsuario = usuario.Nome;
            return new OperationResultGeneric<UsuarioDto> { Success = true, Message = $"O Usuario {nomeUsuario} foi excluido com sucesso." };
        }
    }
}
