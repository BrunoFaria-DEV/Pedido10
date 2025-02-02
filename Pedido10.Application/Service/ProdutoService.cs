using Pedido10.Application.Contract;
using Pedido10.Data.Contract;
using Pedido10.Domain.Dto;
using Pedido10.Domain.Entity;
using Pedido10.Shared.Results.Repository;

namespace Pedido10.Application.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoDto>?> GetAll()
        {
            var produtos = await _produtoRepository.GetAll();
            if (produtos.Count < 1 || !produtos.Any())
            {
                return null;
            }

            List<ProdutoDto> listProdutoDto = [];

            foreach (var produto in produtos)
            {
                listProdutoDto.Add( new ProdutoDto()
                {
                    ID_Produto = produto.ID_Produto,
                    Nome_Produto = produto.Nome_Produto,
                    Descricao = produto.Descricao,
                    Custo_Producao = produto.Custo_Producao,
                    Margem_Lucro = produto.Margem_Lucro,
                    Preco = produto.Preco,
                    QTDE_Estoque = produto.QTDE_Estoque
                });
            }

            return listProdutoDto;
        }

        public async Task<OperationResultGeneric<ProdutoDto>> Find(int id)
        {
            var produto = await _produtoRepository.Find(id);
            if (produto == null) 
            { 
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Produto não encontrado" };
            }

            ProdutoDto produtoDto = new ProdutoDto()
            {
                ID_Produto = produto.ID_Produto,
                Nome_Produto = produto.Nome_Produto,
                Descricao = produto.Descricao,
                Custo_Producao = produto.Custo_Producao,
                Margem_Lucro = produto.Margem_Lucro,
                Preco = produto.Preco,
                QTDE_Estoque = produto.QTDE_Estoque
            };

            return new OperationResultGeneric<ProdutoDto> { Success = true, Message = "Produto encontrado.", Result = produtoDto };
        }

        public async Task<OperationResultGeneric<ProdutoDto>> FindByName(string name)
        {
            var produto = await _produtoRepository.FindByName(name);
            if (produto == null)
            {
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Produto não encontrado" };
            }

            ProdutoDto produtoDto = new ProdutoDto()
            {
                ID_Produto = produto.ID_Produto,
                Nome_Produto = produto.Nome_Produto,
                Descricao = produto.Descricao,
                Custo_Producao = produto.Custo_Producao,
                Margem_Lucro = produto.Margem_Lucro,
                Preco = produto.Preco,
                QTDE_Estoque = produto.QTDE_Estoque
            };

            return new OperationResultGeneric<ProdutoDto> { Success = true, Message = "Produto encontrado", Result = produtoDto };
        }

        public async Task<OperationResultGeneric<ProdutoDto>> Add(ProdutoDto produtoDto)
        {
            Produto produto = new Produto(
                null,
                produtoDto.Nome_Produto,
                produtoDto.Descricao,
                produtoDto.Custo_Producao,
                produtoDto.Margem_Lucro,
                produtoDto.Preco,
                produtoDto.QTDE_Estoque
            ); 

            var resultado = await _produtoRepository.Add(produto);
            if (resultado != true)
            {
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Não foi Possivel incluir o Produto." };
            }

            return new OperationResultGeneric<ProdutoDto> { Success = true, Message = "Produto incluido com sucesso.", Result = produtoDto };
        }

        public async Task<OperationResultGeneric<ProdutoDto>> Update(int id, ProdutoDto produtoDto)
        {
            Produto produto = await _produtoRepository.Find(id);
            if (produto == null)
            {
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Não foi Possivel atualizar o Produto pois não foi encontrado." };
            }

            produto.Nome_Produto = produtoDto.Nome_Produto;
            produto.Descricao = produtoDto.Descricao ?? produto.Descricao;
            produto.Custo_Producao = produtoDto.Custo_Producao ?? produto.Custo_Producao;
            produto.Margem_Lucro = produtoDto.Margem_Lucro ?? produto.Margem_Lucro;
            produto.Preco = produtoDto.Preco;
            produto.QTDE_Estoque = produtoDto.QTDE_Estoque;

            var resultado = await _produtoRepository.Update(produto);
            if (resultado != true)
            {
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Não foi Possivel atualizado o Produto." };
            }

            return new OperationResultGeneric<ProdutoDto> { Success = true, Message = "Produto atualizado com sucesso.", Result = produtoDto };
        }

        public async Task<OperationResultGeneric<ProdutoDto>> Delete(int id)
        {
            Produto produto = await _produtoRepository.Find(id);
            if (produto == null)
            {
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Não foi Possivel excluir o Produto pois não foi encontrado." };
            }

            var resultado = await _produtoRepository.Delete(produto);
            if (resultado != true)
            {
                return new OperationResultGeneric<ProdutoDto> { Success = false, Message = "Não foi Possivel excluir o Produto." };
            }

            string nomeProduto= produto.Nome_Produto;
            return new OperationResultGeneric<ProdutoDto> { Success = true, Message = $"O Produto {nomeProduto} foi excluido com sucesso."};
        }
    }
}
