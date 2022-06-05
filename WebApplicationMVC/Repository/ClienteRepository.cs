using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationMVC.Data;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BancoContext _context;        
        public ClienteRepository(BancoContext context)
        {
            _context = context;
        }

        public ClienteRepository()
        {

        }

        public ClienteModel ListarPorId(int id)
        {
            var cliente = _context.Cliente.FirstOrDefault(c => c.Id == id);
            return cliente;
        }

        public List<ClienteModel> ListarTodos()
        {
            var lista_clientes = _context.Cliente.ToList();
            return lista_clientes;
        }

        public ClienteModel Cadastrar(ClienteModel cliente)
        {
            bool cpfDuplicado = _context.Cliente.Any(x => x.Cpf == cliente.Cpf);

            if (cpfDuplicado)
            {
                throw new Exception("Erro ao cadastrar cliente, cpf duplicado");
            }            

            _context.Cliente.Add(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public ClienteModel Atualizar(ClienteModel cliente)
        {
            var clienteDb = ListarPorId(cliente.Id);
            if(clienteDb == null)
            {
                throw new Exception("Cadastro não encontado!");
            }

            clienteDb.Nome = cliente.Nome;
            clienteDb.Telefone = cliente.Telefone;
            clienteDb.Cpf = cliente.Cpf;
            _context.Cliente.Update(clienteDb);
            _context.SaveChanges();
            return clienteDb;
        }      

        
        public bool Apagar(int id)
        {
            var clienteDb = ListarPorId(id);
            if (clienteDb == null)
            {
                throw new Exception("Cadastro não encontado!");
            }
            _context.Cliente.Remove(clienteDb);
            _context.SaveChanges();

            return true;
        }
    }
}
