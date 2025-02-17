using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;

namespace Pedido10.Application.Service
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public async Task<List<CidadeDto>?> GetAll()
        {
            var cidades = await _cidadeRepository.GetAll();
            if (cidades.Count < 1 || !cidades.Any())
            {
                return null;
            }

            List<CidadeDto> listCidadeDto = [];
            foreach (var cidade in cidades)
            {
                listCidadeDto.Add(new CidadeDto()
                {
                    ID_Cidade = cidade.ID_Cidade,
                    Nome_Cidade = cidade.Nome_Cidade,
                    UF = cidade.UF,
                    Codigo_IBGE = cidade.Codigo_IBGE,
                });
            }

            return listCidadeDto;
        }
    }
}
