using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplicationMVC.Models;
using WebApplicationMVC.Repository;

namespace WebApplicationMVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository db;
        public ClienteController(IClienteRepository context)
        {
            db = context;
        }
        // GET: ClienteController
        public ActionResult Index()
        {
            var cliente = db.ListarTodos();

            return View(cliente);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            var cliente = db.ListarPorId(id);
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var novo_cliente = db.Cadastrar(cliente);
                    return RedirectToAction("Index");
                }

                return View(cliente);
                
            }
            catch
            {
                return View(cliente);
            }             
        }

       
        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = db.ListarPorId(id);

            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Atualizar(cliente);

                    return RedirectToAction("Index");
                }
                return View("Edit", cliente);
            }
            catch
            {
                return View(cliente);
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = db.ListarPorId(id);
            return View(cliente);
            
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ClienteModel cliente)
        {
            try
            {
                bool apagado = db.Apagar(cliente.Id);

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
