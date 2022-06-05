using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationMVC.Data;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public class ContaRepository: IContaRepository
    {
        private readonly BancoContext db;
        
        public ContaRepository()
        {

        }
        public ContaRepository(BancoContext context)
        {
            db = context;
            
        }

        public ContaModel ListarPorId(int id)
        {            
            var contadb = db.Conta.FirstOrDefault(x => x.ClienteId == id);
            return contadb;
        }

        public List<ContaModel> ListarTodos()
        {
            return db.Conta.ToList();
        }

        public ContaModel Cadastrar(ContaModel conta)
        {
            var cliente_nome = db.Cliente.FirstOrDefault(c => c.Id == conta.ClienteId).Nome;
            conta.Nome = cliente_nome;
            db.Conta.Add(conta);
            db.SaveChanges();
            return conta;
        }

        public ContaModel Atualizar(ContaModel conta)
        {
            var contaDb = ListarPorId(conta.Id);

            if (contaDb == null)
            {
                throw new Exception("Cadastro não encontado!");
            }

            var cliente_nome = db.Cliente.FirstOrDefault(c => c.Id == conta.ClienteId).Nome;

            contaDb.Nome = cliente_nome;
            contaDb.ClienteId = conta.ClienteId;
            contaDb.Agencia = conta.Agencia;
            contaDb.Numero = conta.Numero;            

            db.Conta.Update(contaDb);
            db.SaveChanges();

            return contaDb;
        }

        public bool Apagar(int id)
        {
            var conta = ListarPorId(id);
            if (conta == null)
            {
                throw new Exception("Cadastro não encontado!");
            }

            db.Conta.Remove(conta);
            db.SaveChanges();

            return true;
        }
    }
}
