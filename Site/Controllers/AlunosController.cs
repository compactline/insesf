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

namespace Site.Controllers
{
    public class AlunosController : Controller
    {
        private SiteDBContext db = new SiteDBContext();


        public ActionResult NovoAluno()
        {
            if (Session["Aluno"] != null)
            {
                return View("Edit", Session["Aluno"]);
            }
            return View();
        }
        
        // POST: Alunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoAluno([Bind(Include = "ID,Nome,NomeGenitora,DataNascimento,Email,Endereco,Bairro, CEP, UF, Cidade,Telefone,CPF,RG,Observacao,Senha,DataCadastro")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.CPF = aluno.CPF.Replace("-", "").Replace(".", "");

                if ( db.Alunos.Where(a=>a.CPF.Equals(aluno.CPF)).Count()>0 ){
                    ViewBag.Mensagem = "CPF já cadastrado.";
                    return View(aluno);
                }
                aluno.DataCadastro = DateTime.Now;
                aluno.Senha = CryptSharp.MD5Crypter.MD5.Crypt(aluno.Senha);
                aluno.CEP = aluno.CEP.Replace("-", "").Replace(".", "");
                aluno.Telefone = aluno.Telefone.Replace("-", "").Replace(")", "").Replace("(", "");
                db.Alunos.Add(aluno);
                
                db.SaveChangesAsync();
                Session["Aluno"] = aluno;
                return RedirectToAction("Index","Inscricao");
            }

            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<ActionResult> MeusDados()
        {
            if (Session["Aluno"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Aluno aluno = (Aluno)Session["Aluno"];
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MeusDados([Bind(Include = "ID,Nome,NomeGenitora,DataNascimento,Email,Endereco,Cidade,Telefone,CPF,RG,Observacao,Senha,DataCadastro")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Session["Aluno"] = aluno;
                return RedirectToAction("Index");
            }
            return View(aluno);
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
