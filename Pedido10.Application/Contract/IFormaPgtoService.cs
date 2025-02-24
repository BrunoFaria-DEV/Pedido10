using Pedido10.Domain.Dto;
using Pedido10.Shared.Results.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Application.Contract
{
    public interface IFormaPgtoService
    {
        public Task<List<Forma_PGTO_Dto>?> GetAll();
        public Task<OperationResultGeneric<Forma_PGTO_Dto>> Find(int id);
        public Task<OperationResultGeneric<Forma_PGTO_Dto>> FindByName(string descFormaPgto);
        public Task<OperationResultGeneric<Forma_PGTO_Dto>> Add(Forma_PGTO_Dto formaPgtoDto);
        public Task<OperationResultGeneric<Forma_PGTO_Dto>> Update(int id, Forma_PGTO_Dto formaPgtoDto);
        public Task<OperationResultGeneric<Forma_PGTO_Dto>> Delete(int id);
    }
}
