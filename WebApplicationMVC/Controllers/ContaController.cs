using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using WebApplicationMVC.Data;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository;

namespace WebApplicationMVC.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaRepository db;
        private readonly IClienteRepository _cliente;

        public ContaController(IContaRepository context, IClienteRepository i_cliente)
        {
            db = context;
            _cliente = i_cliente;


        }

        // GET: ContaController
        public IActionResult Index()
        {
            var conta = db.ListarTodos();

            return View(conta);
        }

       
        // GET: ContaController/Details/5
        public ActionResult Details(int id)
        {
            var conta = db.ListarPorId(id);
            return View(conta);
        }

        public IActionResult Create()
        {

            var lista = _cliente.ListarTodos();

            ViewBag.ClienteId = new SelectList(lista, "Id", "Nome");


            return View();
        }

        // POST: ContaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ClienteId,Agencia,Numero,Saldo")] ContaModel conta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    var nova_conta = db.Cadastrar(conta);                    
                    
                    return RedirectToAction("Index");
                }                
                
                var lista = _cliente.ListarTodos();                

                ViewBag.ClienteId = new SelectList(lista, "Id", "Nome", conta.ClienteId);
                

                return View(conta);

            }
            catch
            {
                return View(conta);
            }
        }


        // GET: ContaController/Edit/5
        public ActionResult Edit(int id)
        {
            var conta = db.ListarPorId(id);

            return View(conta);
        }

        // POST: ContaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContaModel conta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Atualizar(conta);

                    return RedirectToAction("Index");
                }
                return View("Edit", conta);
            }
            catch
            {
                return View(conta);
            }
        }

        // GET: ContaController/Delete/5
        public ActionResult Delete(int id)
        {
            var conta = db.ListarPorId(id);
            return View(conta);

        }

        // POST: ContaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ContaModel conta)
        {
            try
            {
                bool apagado = db.Apagar(conta.Id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Erro ao tentar apagar o contato!";
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["MensagemErro"] = $"Erro ao tentar apagar o contato! Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        
    }
}
