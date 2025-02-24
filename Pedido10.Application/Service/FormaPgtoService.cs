using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Pedido10.Shared.Results.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Application.Service
{
    public class FormaPgtoService : IFormaPgtoService
    {
        private readonly IFormaPgtoRepository _formaPgtoRepository;

        public FormaPgtoService(IFormaPgtoRepository formaPgtoRepository)
        {
            _formaPgtoRepository = formaPgtoRepository;
        }

        public async Task<List<Forma_PGTO_Dto>?> GetAll()
        {
            var formaPgtos = await _formaPgtoRepository.GetAll();
            if (formaPgtos == null)
            {
                return null;
            }

            List<Forma_PGTO_Dto> formasPgto = [];

            foreach (var formaPgto in formaPgtos)
            {
                formasPgto.Add(new Forma_PGTO_Dto()
                {
                    ID_Forma_PGTO = formaPgto.ID_Forma_PGTO,
                    Desc_Forma_PGTO = formaPgto.Desc_Forma_PGTO
                });
            }

            return formasPgto;
        }

        public async Task<OperationResultGeneric<Forma_PGTO_Dto>> Find(int id)
        {
            var formaPgto = await _formaPgtoRepository.Find(id);
            if (formaPgto == null)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO não foi encontrado." };
            }

            var Forma_PGTO_DtoAtualizado = new Forma_PGTO_Dto()
            {
                ID_Forma_PGTO = formaPgto.ID_Forma_PGTO,
                Desc_Forma_PGTO = formaPgto.Desc_Forma_PGTO
            };
            return new OperationResultGeneric<Forma_PGTO_Dto> { Success = true, Message = $"A forma de pagamento {Forma_PGTO_DtoAtualizado.Desc_Forma_PGTO} foi encontrado", Result = Forma_PGTO_DtoAtualizado };
        }

        public async Task<OperationResultGeneric<Forma_PGTO_Dto>> FindByName(string descFormaPgto)
        {
            var formaPgto = await _formaPgtoRepository.FindByName(descFormaPgto);
            if (formaPgto == null)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO não foi encontrado." };
            }

            var Forma_PGTO_DtoAtualizado = new Forma_PGTO_Dto()
            {
                ID_Forma_PGTO = formaPgto.ID_Forma_PGTO,
                Desc_Forma_PGTO = formaPgto.Desc_Forma_PGTO
            };

            return new OperationResultGeneric<Forma_PGTO_Dto> { Success = true, Message = $"A forma de pagamento {Forma_PGTO_DtoAtualizado.Desc_Forma_PGTO} foi encontrado", Result = Forma_PGTO_DtoAtualizado };
        }

        public async Task<OperationResultGeneric<Forma_PGTO_Dto>> Add(Forma_PGTO_Dto formaPgtoDto)
        {
            var formaPgto = new Forma_PGTO() { Desc_Forma_PGTO = formaPgtoDto.Desc_Forma_PGTO };

            var addResult = await _formaPgtoRepository.Add(formaPgto);

            if (addResult == false)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO não foi adicionado." };
            }

            var Forma_PGTO_DtoAtualizado = new Forma_PGTO_Dto()
            {
                ID_Forma_PGTO = formaPgto.ID_Forma_PGTO,
                Desc_Forma_PGTO = formaPgto.Desc_Forma_PGTO
            };

            return new OperationResultGeneric<Forma_PGTO_Dto> { Success = true, Message = $"A forma de pagamento {Forma_PGTO_DtoAtualizado.Desc_Forma_PGTO} foi incluido.", Result = Forma_PGTO_DtoAtualizado };
        }

        public async Task<OperationResultGeneric<Forma_PGTO_Dto>> Update(int id, Forma_PGTO_Dto formaPgtoDto)
        {
            var formaPgto = await _formaPgtoRepository.Find(id);
            if (formaPgto == null)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO a ser atualizado não foi encontrado." };
            }

            formaPgto.Desc_Forma_PGTO = formaPgtoDto.Desc_Forma_PGTO ?? formaPgto.Desc_Forma_PGTO;

            var updateResult = await _formaPgtoRepository.Update(formaPgto);
            if (updateResult != true)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO não pode ser atualizado." };
            }

            var Forma_PGTO_DtoAtualizado = new Forma_PGTO_Dto()
            {
                ID_Forma_PGTO = formaPgto.ID_Forma_PGTO,
                Desc_Forma_PGTO = formaPgto.Desc_Forma_PGTO
            };

            return new OperationResultGeneric<Forma_PGTO_Dto> { Success = true, Message = $"A forma de pagamento {Forma_PGTO_DtoAtualizado.Desc_Forma_PGTO} foi atualizado.", Result = Forma_PGTO_DtoAtualizado };
        }

        public async Task<OperationResultGeneric<Forma_PGTO_Dto>> Delete(int id)
        {
            var formaPgto = await _formaPgtoRepository.Find(id);
            if (formaPgto == null)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO a ser excluido não foi encontrado." };
            }

            var deleteResult = await _formaPgtoRepository.Delete(formaPgto);
            if (deleteResult != true)
            {
                return new OperationResultGeneric<Forma_PGTO_Dto> { Success = false, Message = "O Forma_PGTO não pode ser excluido." };
            }

            string nomeForma_PGTO = formaPgto.Desc_Forma_PGTO;

            return new OperationResultGeneric<Forma_PGTO_Dto> { Success = true, Message = $"A forma de pagamento {nomeForma_PGTO} foi excluido do sistema." };
        }
    }
}
