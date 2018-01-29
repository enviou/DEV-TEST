using MuralDeRecados.DAL;
using MuralDeRecados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuralDeRecados.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepositorio usuarioRep = new UsuarioRepositorio();

        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuarioRep.InserirUsuario(usuario);
                    Session["usuarioLogado"] = usuario;
                    RedirectToAction("Index", "Murais");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Erro ao tentar criar novo usuário.");
                }
            }

            return RedirectToAction("Index", "Murais");
        }
    }
}