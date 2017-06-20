using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Site.Context;
using Site.Models;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Exception;

namespace Site.Controllers
{
    public class InscricaoController : Controller
    {
        private SiteDBContext db = new SiteDBContext();
        private bool sandbox = false;
        public async Task<ActionResult> Index(string id)
        {

            if (Session["IdCurso"] == null || (id!=null && !id.Equals(Session["IdCurso"]) ) )
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Curso curso = await db.Cursos.Where(c => c.Codigo.Equals(id)).FirstOrDefaultAsync();
                if (curso == null)
                {
                    return Content("Nenhum curso foi selecionado") ;
                }
                Session["IdCurso"] = curso.ID;
                Session["TituloCurso"] = curso.Titulo;

            }
            
            
            Aluno aluno = (Aluno)Session["Aluno"] ;
            if ( aluno == null)
            {
                return RedirectToAction("NovoAluno","Alunos");
            }

          //  List<Matricula> matriculas = db.Matriculas.Where(m => m.Situacao.Equals("I") && m.IDAluno == aluno.ID).ToList();
          //  ViewBag.matriculas = matriculas;
            return View();
        }
        public async Task<ActionResult> Confirmar()
        {

            if (Session["IdCurso"] == null)
            {
                return View();
            }
            var id = Session["IdCurso"];
            Curso curso = await db.Cursos.FindAsync(id);
            if (curso == null)
            {
                return HttpNotFound();
            }

            if (curso.FimInscricao < DateTime.Now || curso.DataInicio.AddDays(7) < DateTime.Now)
            {
                ViewBag.Mensagem = "O período de inscrições já finalizou.";
                return View();
            }

            int totalInscritos = db.Matriculas.Where(m => !m.Situacao.Equals("C") && m.IDCurso == curso.ID).Count();
            if (totalInscritos >= curso.Vagas)
            {
                ViewBag.Mensagem = "Não há mais vagas dispononíveis para o curso.";
                return View();
            }

            if (Session["Aluno"] == null)
            {
                return RedirectToAction("NovoAluno", "Alunos");
            }

            Aluno aluno = (Aluno) Session["Aluno"] ;
            var transacao = db.Database.BeginTransaction();
            try
            {
                Matricula matricula = new Matricula();
                matricula.IDAluno = aluno.ID;
                matricula.IDCurso = curso.ID;
                matricula.DataMatricula = DateTime.Now;
                matricula.Situacao = "I"; //Inscrito

                db.Matriculas.Add(matricula);
                db.SaveChanges();


                string plano = "Cursos = " + curso.Titulo;
                string nome = aluno.Nome;
                string telefone = aluno.Telefone;
                string uf = aluno.UF.ToUpper();
                string cidade = aluno.Cidade;
                string bairro = aluno.Bairro;
                string cep = aluno.CEP;
                string endereco = aluno.Endereco;
                string cpf = aluno.CPF;


                PaymentRequest payment = new PaymentRequest();

                payment.Items.Add(new Item("0001", plano, 1, curso.Valor));

                payment.Sender = new Sender(
                                        nome,
                                        User.Identity.Name,
                                        new Phone(
                                            telefone.Substring(0, 2),
                                            telefone.Substring(2)
                                        )
                );


                payment.Shipping = new Shipping();
                payment.Shipping.ShippingType = ShippingType.NotSpecified;

                payment.Shipping.Address = new Address(
                    "BRA",
                    uf,
                    cidade,
                    bairro,
                    cep,
                    endereco,
                    "0",
                    null
                );

                payment.Currency = Currency.Brl;

                payment.Reference = "MAT" + matricula.ID.ToString();

                SenderDocument senderDocument = senderDocument = new SenderDocument(Documents.GetDocumentByType("CPF"), cpf);

                payment.Sender.Documents.Add(senderDocument);

                payment.RedirectUri = new Uri("http://www.insesf.com/Inscricao/RetornoPagseguro");

                payment.NotificationURL = "http://www.insesf.com/RetornoPagamento/Retorno";

                AccountCredentials credentials = PagSeguroConfiguration.Credentials(sandbox);
                
                Uri paymentRedirectUri = payment.Register(credentials);
                string url = paymentRedirectUri.AbsoluteUri;
                transacao.Commit();
                Session["IdCurso"] = null;
                Session["TituloCurso"] = null; 
                return Redirect(url);
            }
            catch (PagSeguroServiceException e)
            {
                transacao.Rollback();
                ViewBag.Mensagem = string.Format("Não foi possível processar a matrícula. Tente de novo mais tarde. {0}", e.ToString());
                return View();

            }
        }

        public ActionResult RetornoPagseguro()
        {
            return View();
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