using Site.Context;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    
    public class HomeController : Controller
    {
        private SiteDBContext db = new SiteDBContext();
        public ActionResult Index()
        {
            ViewBag.Title="AWD Cursos";

            DateTime hoje = DateTime.Now;
            List<Curso> cursos = db.Cursos.Where(c => hoje >= c.InicioInscricao && hoje<=c.FimInscricao).Take(3).ToList();
            ViewBag.cursos = cursos;
            return View();
        }

        public ActionResult Acesso(string email, string senha)
        {
            ViewBag.Title = "AWD Cursos";

            Aluno aluno = db.Alunos.Where(a => a.Email.Equals(email)).FirstOrDefault();
            if (aluno != null)
            {
                if (CryptSharp.MD5Crypter.CheckPassword(senha, aluno.Senha))
                {
                    Session["Aluno"] = aluno;
                    return RedirectToAction("Index", "Inscricao");

                }
                else
                {
                    ViewBag.Mensagem = "Login inválido.";
                }
            }
            else
            {
                ViewBag.Mensagem = "Usuário não encontrado.";
            }

            
            return View();
        }

        [HttpPost]
        public ActionResult Contato(string nome, string email, string telefone, string mensagem)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("contato@awdcursos.com.br");
                msg.To.Add(new MailAddress("contato@awdcursos.com.br"));
                msg.Subject = "Contato AWDCursos";
                string text = "Contato: " + nome + "<br>";
                text += "E-mail: " + email + "<br>";
                text += "Telefone: " + telefone + "<br>";
                text += "Mensagem: " + mensagem;
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mensagem, null, MediaTypeNames.Text.Html));

                SmtpClient smtpClient = new SmtpClient("mail.awdcursos.com.br");
                //smtpClient.Credentials = new System.Net.NetworkCredential("yourusername", "yourpassword");
                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
                return Content("Mensagem enviada com sucesso");
            }
            catch (Exception e)
            {
                return Content("Sua mensagem não foi enviada. Tente mais tarde.");
            }

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}