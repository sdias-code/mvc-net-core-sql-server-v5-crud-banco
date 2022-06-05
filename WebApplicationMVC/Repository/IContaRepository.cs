using System.Collections.Generic;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public interface IContaRepository
    {
        public ContaModel Cadastrar(ContaModel conta);
        public List<ContaModel> ListarTodos();
        public ContaModel ListarPorId(int id);
        public ContaModel Atualizar(ContaModel conta);
        public bool Apagar(int id);
    }
}
