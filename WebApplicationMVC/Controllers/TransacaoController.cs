using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository;

namespace WebApplicationMVC.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ITransacaoRepository _transacao;
        private readonly IClienteRepository _cliente;

        public TransacaoController(ITransacaoRepository iTransacao, IClienteRepository iCliente)
        {
            _transacao = iTransacao;
            _cliente = iCliente;
        }

        // GET: TransacaoController
        public ActionResult Index()
        {
            var listaTransacao = _transacao.ListarTodos();

            return View(listaTransacao);
        }



        // GET: TransacaoController/Create
        public ActionResult Create()
        {
            var lista_cliente = _cliente.ListarTodos();

            var listaOperacoes = new[]
                {
                    new SelectListItem { Value = "Depósito", Text = "Depósito" },
                    new SelectListItem { Value = "Saque", Text = "Saque" }
                };

            ViewBag.TipoOperacao = new SelectList(listaOperacoes, "Value", "Text");

            ViewBag.ClienteId = new SelectList(lista_cliente, "Id", "Nome");
            

            return View();
        }

        // POST: TransacaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ClienteId, Nome, TipoOperacao, Data, Valor")] TransacaoModel transacao)
        {
            try
            {
                var lista = _cliente.ListarTodos();                

                ViewBag.ClienteId = new SelectList(lista, "Id", "Nome", transacao.ClienteId);

                var listaOperacoes = new[]
                {
                    new SelectListItem { Value = "Depósito", Text = "Depósito" },
                    new SelectListItem { Value = "Saque", Text = "Saque" }
                };

                ViewBag.TipoOperacao = new SelectList(listaOperacoes, "Value", "Text", transacao.TipoOperacao);

                if (transacao.TipoOperacao == "Depósito")
                {
                    _transacao.Depositar(transacao);
                }

                if (transacao.TipoOperacao == "Saque")
                {
                    _transacao.Sacar(transacao);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        // GET: TransacaoController/Delete/5
        public ActionResult Delete(int id)
        {
            var transacao = _transacao.ListarPorId(id);

            return View(transacao);
        }

        // POST: TransacaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TransacaoModel transacao)
        {
            
            try
            {
                bool apagado = _transacao.Apagar(id);

                if (apagado)
                {
                    TempData["sucesso"] = "Transação apagada com sucesso!";
                }
                else
                {
                    TempData["erro"] = $"Erro ao tentar apagar transação!";
                }
               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
