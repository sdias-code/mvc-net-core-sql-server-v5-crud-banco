using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationMVC.Data;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly BancoContext db;
        private readonly ContaRepository _conta;
        private readonly ClienteRepository _cliente;
        public TransacaoRepository(BancoContext context)
        {
            db = context;
            _conta = new ContaRepository();
            _cliente = new ClienteRepository();
        }

        public TransacaoModel ListarPorId(int id)
        {
            var transacao = db.Transacao.FirstOrDefault(t => t.Id == id);
            return transacao;
        }

        public List<TransacaoModel> ListarTodos()
        {
            var listaTransacao = db.Transacao.ToList();
            return listaTransacao;
        }

        public TransacaoModel Depositar(TransacaoModel transacao)
        {

            var contaDb = db.Conta.FirstOrDefault(c => c.Id == transacao.ClienteId);

            if (transacao.Valor <= 0)
            {
                throw new Exception("Não é possível depositar valor negativo!");
            }

            contaDb.Saldo = contaDb.Saldo + transacao.Valor;

            var nome = db.Cliente.FirstOrDefault(c => c.Id == transacao.ClienteId).Nome;

            var nova_transacao = new TransacaoModel()
            {
                ClienteId = contaDb.ClienteId,
                Nome = nome,
                TipoOperacao = transacao.TipoOperacao,
                Data = DateTime.Now,
                Valor = transacao.Valor

            };

            db.Transacao.Add(nova_transacao);
            db.SaveChanges();

            return nova_transacao;
        }


        public TransacaoModel Sacar(TransacaoModel transacao)
        {

            var contaDb = db.Conta.FirstOrDefault(c => c.Id == transacao.ClienteId);

            if (transacao.Valor > contaDb.Saldo || transacao.Valor < 0)
            {                
                throw new Exception("Não é possível depositar valor negativo!");
            }

            contaDb.Saldo = contaDb.Saldo - transacao.Valor;

            var nome = db.Cliente.FirstOrDefault(c => c.Id == transacao.ClienteId).Nome;

            var nova_transacao = new TransacaoModel()
            {
                ClienteId = contaDb.ClienteId,
                Nome = nome,
                TipoOperacao = transacao.TipoOperacao,
                Data = DateTime.Now,
                Valor = transacao.Valor
            };

            db.Transacao.Add(nova_transacao);
            db.SaveChanges();

            return nova_transacao;

        }

        public bool Apagar(int id)
        {
            var transacao = ListarPorId(id);
            if (transacao == null)
            {
                throw new Exception("Cadastro não encontado!");
            }

            db.Transacao.Remove(transacao);
            db.SaveChanges();

            return true;
        }
    }
}
