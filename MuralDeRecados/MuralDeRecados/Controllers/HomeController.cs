using MuralDeRecados.DAL;
using MuralDeRecados.Models;
using MuralDeRecados.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuralDeRecados.Controllers
{
    public class HomeController : Controller
    {

        private UsuarioRepositorio usuarioRep = new UsuarioRepositorio();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            if (Session["usuarioLogado"] == null)
                return View();

            return RedirectToAction("Index", "Murais", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModelApp u)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid)
            {
                Usuario usuario = usuarioRep.ObterLogin(u.Email, CriptografiaService.Encrypt(u.Senha));
                if (usuario != null)
                {
                    Session["usuarioLogado"] = usuario;
                    return RedirectToAction("Index", "Murais");
                }
                else
                {
                    ModelState.AddModelError("", "Login ou Senha inválidos!");
                }
            }
            return View(u);
        }

        public ActionResult Logoff()
        {
            if (Session["usuarioLogado"] != null)
            {
                Session.Remove("usuarioLogado");
            }

            return View("Login");
        }

    }
}