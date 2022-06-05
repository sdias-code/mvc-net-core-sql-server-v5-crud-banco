using System.Collections.Generic;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public interface IClienteRepository
    {
        public ClienteModel Cadastrar(ClienteModel cliente);
        public List<ClienteModel> ListarTodos();
        public ClienteModel ListarPorId(int id);
        public ClienteModel Atualizar(ClienteModel cliente);
        public bool Apagar(int id);

    }
}
