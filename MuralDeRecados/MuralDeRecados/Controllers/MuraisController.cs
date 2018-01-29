using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MuralDeRecados.Models;
using MuralDeRecados.DAL;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace MuralDeRecados.Controllers
{
    public class MuraisController : Controller
    {
        private MuralRepositorio muralRep = new MuralRepositorio();

        // GET: Murais
        public ActionResult Index()
        {
            var murals = muralRep.ObterMurais(includeProperties: "Usuario", orderBy: m => m.OrderByDescending(d => d.DataCriacao));

            bool logado = Session["usuarioLogado"] != null;
            ViewBag.Ocultar = "ocultar";

            if (logado)
            {
                ViewBag.Ocultar = "";           
            }

            ViewBag.Exibir = murals.Count() > 0 ? "ocultar" : "";

            return View(murals.ToList());            
        }

        // POST: Murais/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Salvar(string texto)//[Bind(Include = "MuralId,Texto,DataCriacao,UsuarioId")] 
        {
            Usuario usuario = (Usuario)Session["usuarioLogado"];
            Mural mural = new Mural() { DataCriacao = DateTime.Now, Texto = texto,
                                        UsuarioId = usuario.UsuarioId };
            try
            {
                muralRep.InserirMural(mural);

                TempData["TipoMsg"] = "text-success";
                TempData["Msg"] = "Mensagem inserida com sucesso.";

                //EMAIL
                var body = @"<p>Olá, <strong>{0}</strong></p>.<p>Uma nova mensagem foi postada no Mural</p><p>Mensagem:</p><p>{1}</p>";

                var message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["EmailEnvio"], "Sistema de Mural");

                List<string> listaEmails = (from s in new UsuarioRepositorio().ObterUsuarios()
                                            where s.UsuarioId != usuario.UsuarioId
                                            select s.Email).ToList();

                if (listaEmails.Count > 0)
                {
                    foreach (string email in listaEmails)
                    {
                        message.Bcc.Add(email);
                    }

                    message.Subject = "Novo Post!!";
                    message.Body = string.Format(body, usuario.Nome, mural.Texto);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        smtp.Send(message);
                    }
                }

                muralRep.Salvar();
            }
            catch (Exception ex)
            {
                TempData["TipoMsg"] = "text-danger";
                TempData["Msg"] += " Erro: " + ex.Message;
                return Json(new { erro = "Erro ao tentar enviar." });
            }

            return Json(new { mural = mural.Texto });
        }
    }
}
