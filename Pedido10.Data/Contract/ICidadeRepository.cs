using MathNet.Numerics;
using Pedido10.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido10.Data.Contract
{
    public interface ICidadeRepository
    {   
        Task<List<Cidade>> GetAll();
        Task<Cidade> Find(int id);
        Task<List<Cidade>> FindByName(string name);
        Task<List<Cidade>> FindByUf(string uf);
    }
}