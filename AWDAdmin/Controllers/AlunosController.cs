using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AWDAdmin.Context;
using AWDAdmin.Models;

namespace AWDAdmin.Controllers
{
    [Authorize]
    public class AlunosController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Alunos
        public ActionResult Index()
        {
            return View(db.Alunos.ToList());
        }

        // GET: Alunos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // GET: Alunos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,NomeGenitora,DataNascimento,Email,Endereco,Bairro,CEP,UF,Cidade,Telefone,CPF,RG,Observacao,Senha,DataCadastro")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Alunos.Add(aluno);
                db.SaveChanges();
                ViewBag.Mensagem = "Dados salvos com sucesso";
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível salvar os dados.";
            }

            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
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
        public ActionResult Edit([Bind(Include = "ID,Nome,NomeGenitora,DataNascimento,Email,Endereco,Bairro,CEP,UF,Cidade,Telefone,CPF,RG,Observacao,Senha,DataCadastro")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.Entry(aluno).Property(a => a.Senha).IsModified = false;
                db.Entry(aluno).Property(a => a.DataCadastro).IsModified = false;
                db.SaveChanges();
                ViewBag.Mensagem = "Dados salvos com sucesso";
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível salvar os dados.";
            }
            return View(aluno);
        }

        // GET: Alunos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Alunos.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Alunos.Find(id);
            db.Alunos.Remove(aluno);
            db.SaveChanges();
            ViewBag.Mensagem = "Registro removido com sucesso";
            return View(new Aluno());
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
