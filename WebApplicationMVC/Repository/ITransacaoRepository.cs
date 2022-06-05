using System.Collections.Generic;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public interface ITransacaoRepository
    {
        
        public List<TransacaoModel> ListarTodos();
        public TransacaoModel ListarPorId(int id);
        public TransacaoModel Depositar(TransacaoModel transacao);
        public TransacaoModel Sacar(TransacaoModel transacao);
        public bool Apagar(int id);

    }
}
