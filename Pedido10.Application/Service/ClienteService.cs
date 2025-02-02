using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Pedido10.Shared.Results.Repository;

namespace Pedido10.Application.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        { 
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteDto>?> GetAll()
        {
            var clientes = await _clienteRepository.GetAll();
            if (clientes == null)
            {
                return null;
            }

            List<ClienteDto> clientesDto = [];

            foreach (var cliente in clientes)
            {
                clientesDto.Add( new ClienteDto()
                {
                    ID_Cliente = cliente.ID_Cliente,
                    Tipo_Pessoa = cliente.Tipo_Pessoa,
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    CNPJ = cliente.CNPJ,
                    Nascimento = cliente.Nascimento,
                    Email = cliente.Email,
                    Telefone = cliente.Telefone,
                    Endereco = cliente.Endereco,
                    Localizador = cliente.Localizador,
                    ID_Cidade = cliente.ID_Cidade,
                });
            }

            return clientesDto;
        }

        public async Task<OperationResultGeneric<ClienteDto>> Find(int id)
        {
            var cliente = await _clienteRepository.Find(id);
            if (cliente == null)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente não foi encontrado." };
            }

            var ClienteDtoAtualizado = new ClienteDto()
            {
                ID_Cliente = cliente.ID_Cliente,
                Tipo_Pessoa = cliente.Tipo_Pessoa,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                CNPJ = cliente.CNPJ,
                Nascimento = cliente.Nascimento,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                Endereco = cliente.Endereco,
                Localizador = cliente.Localizador,
                ID_Cidade = cliente.ID_Cidade,
            };
            return new OperationResultGeneric<ClienteDto> { Success = true, Message = $"O cliente {ClienteDtoAtualizado.Nome} foi encontrado", Result = ClienteDtoAtualizado };
        }

        public async Task<OperationResultGeneric<ClienteDto>> FindByEmail(string email)
        {
            var cliente = await _clienteRepository.FindByEmail(email);
            if (cliente == null)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente não foi encontrado." };
            }

            var ClienteDtoAtualizado = new ClienteDto()
            {
                ID_Cliente = cliente.ID_Cliente,
                Tipo_Pessoa = cliente.Tipo_Pessoa,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                CNPJ = cliente.CNPJ,
                Nascimento = cliente.Nascimento,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                Endereco = cliente.Endereco,
                Localizador = cliente.Localizador,
                ID_Cidade = cliente.ID_Cidade,
            };
            return new OperationResultGeneric<ClienteDto> { Success = true, Message = $"O cliente {ClienteDtoAtualizado.Nome} foi encontrado", Result = ClienteDtoAtualizado };
        }

        public async Task<OperationResultGeneric<ClienteDto>> Add(ClienteDto clienteDto)
        {
            var cliente = new Cliente(clienteDto);
            var addResult = await _clienteRepository.Add(cliente);

            if (addResult != true)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente não foi encontrado." };
            }

            var ClienteDtoAtualizado = new ClienteDto()
            {
                ID_Cliente = cliente.ID_Cliente,
                Tipo_Pessoa = cliente.Tipo_Pessoa,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                CNPJ = cliente.CNPJ,
                Nascimento = cliente.Nascimento,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                Endereco = cliente.Endereco,
                Localizador = cliente.Localizador,
                ID_Cidade = cliente.ID_Cidade,
            };

            return new OperationResultGeneric<ClienteDto> { Success = true, Message = $"O cliente {ClienteDtoAtualizado.Nome} foi incluido.", Result = ClienteDtoAtualizado };
        }

        public async Task<OperationResultGeneric<ClienteDto>> Update(int id, ClienteDto clienteDto)
        {
            var cliente = await _clienteRepository.Find(id);
            if (cliente == null)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente a ser atualizado não foi encontrado." };
            }

            cliente.Tipo_Pessoa = clienteDto.Tipo_Pessoa;
            if (clienteDto.Tipo_Pessoa == true)
            {
                cliente.CPF = clienteDto.CPF ?? cliente.CPF;
                cliente.CNPJ = null;
            }
            else
            {
                cliente.CPF = null;
                cliente.CNPJ = clienteDto.CNPJ ?? cliente.CNPJ;
            }
            cliente.Nome = clienteDto.Nome ?? cliente.Nome;
            cliente.Nascimento = clienteDto.Nascimento ?? cliente.Nascimento;
            cliente.Email = clienteDto.Email ?? cliente.Email;
            cliente.Telefone = clienteDto.Telefone ?? cliente.Telefone;
            cliente.Endereco = clienteDto.Endereco ?? cliente.Endereco;
            cliente.Localizador = clienteDto.Localizador ?? cliente.Localizador;
            // buscar cidade para não ter erros
            cliente.ID_Cidade = clienteDto.ID_Cidade ?? cliente.ID_Cidade;

            var updateResult = await _clienteRepository.Update(cliente);
            if (updateResult != true)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente não pode ser atualizado." };
            }

            var ClienteDtoAtualizado = new ClienteDto()
            {
                ID_Cliente = cliente.ID_Cliente,
                Tipo_Pessoa = cliente.Tipo_Pessoa,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                CNPJ = cliente.CNPJ,
                Nascimento = cliente.Nascimento,
                Email = cliente.Email,
                Telefone = cliente.Telefone,
                Endereco = cliente.Endereco,
                Localizador = cliente.Localizador,
                ID_Cidade = cliente.ID_Cidade,
            };

            return new OperationResultGeneric<ClienteDto> { Success = true, Message = $"O cliente {ClienteDtoAtualizado.Nome} foi atualizado.", Result = ClienteDtoAtualizado };
        }

        public async Task<OperationResultGeneric<ClienteDto>> Delete(int id)
        {
            var cliente = await _clienteRepository.Find(id);
            if (cliente == null)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente a ser excluido não foi encontrado." };
            }

            var deleteResult = await _clienteRepository.Delete(cliente);
            if (deleteResult != true)
            {
                return new OperationResultGeneric<ClienteDto> { Success = false, Message = "O Cliente não pode ser excluido." };
            }

            string nomeCliente = cliente.Nome;

            return new OperationResultGeneric<ClienteDto> { Success = true, Message = $"O cliente {nomeCliente} foi excluido do sistema." };
        }
    }
}
