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
    public class ProfessorsController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Professors
        public ActionResult Index()
        {
            var profesores = db.Professores.Include(p => p.Pessoa);
            return View(profesores.ToList());
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professores.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professors/Create
        public ActionResult Create()
        {
            ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "Nome");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Curriculo,PessoaId")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professores.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PessoaId = new SelectList(db.Pessoas, "Id", "Nome", professor.PessoaId);
            return View(professor);
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professores.Where(c => c.Pessoa.Id == id.Value).FirstOrDefault();
            if (professor == null)
            {
                professor = new Professor();
                professor.Pessoa = db.Pessoas.Find(id);
                professor.PessoaId = id.Value;
            }
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Curriculo, PessoaId")] Professor professor)
        {

            Professor prof= db.Professores.Find(professor.Id);

            if (prof == null)
            {
                db.Professores.Add(prof);
                db.SaveChanges();
            }
            else
            {

                if (ModelState.IsValid)
                {
                    prof.Curriculo = professor.Curriculo;
                    db.Entry(prof).State = EntityState.Modified;
                    db.Entry(prof).Property("PessoaId").IsModified = false;

                    db.SaveChanges();
                }
            }
            return View(professor);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professores.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professores.Find(id);
            db.Professores.Remove(professor);
            db.SaveChanges();
            return RedirectToAction("Index");
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
