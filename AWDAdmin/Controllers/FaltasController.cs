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
    public class FaltasController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Faltas
        public ActionResult Index()
        {
            var faltas = db.Faltas.Include(f => f.Aluno).Include(f => f.Disciplina);
            return View(faltas.ToList());
        }

        // GET: Faltas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Falta falta = db.Faltas.Find(id);
            if (falta == null)
            {
                return HttpNotFound();
            }
            return View(falta);
        }

        // GET: Faltas/Create
        public ActionResult Create()
        {
            ViewBag.Aluno_Id = new SelectList(db.Alunos, "ID", "Nome");
            ViewBag.Disciplina_Id = new SelectList(db.Disciplinas, "Id", "Nome");
            return View();
        }

        // POST: Faltas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Data,Disciplina_Id,Aluno_Id")] Falta falta)
        {
            if (ModelState.IsValid)
            {
                db.Faltas.Add(falta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Aluno_Id = new SelectList(db.Alunos, "ID", "Nome", falta.Aluno_Id);
            ViewBag.Disciplina_Id = new SelectList(db.Disciplinas, "Id", "Nome", falta.Disciplina_Id);
            return View(falta);
        }

        // GET: Faltas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Falta falta = db.Faltas.Find(id);
            if (falta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Aluno_Id = new SelectList(db.Alunos, "ID", "Nome", falta.Aluno_Id);
            ViewBag.Disciplina_Id = new SelectList(db.Disciplinas, "Id", "Nome", falta.Disciplina_Id);
            return View(falta);
        }

        // POST: Faltas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,Disciplina_Id,Aluno_Id")] Falta falta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(falta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Aluno_Id = new SelectList(db.Alunos, "ID", "Nome", falta.Aluno_Id);
            ViewBag.Disciplina_Id = new SelectList(db.Disciplinas, "Id", "Nome", falta.Disciplina_Id);
            return View(falta);
        }

        // GET: Faltas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Falta falta = db.Faltas.Find(id);
            if (falta == null)
            {
                return HttpNotFound();
            }
            return View(falta);
        }

        // POST: Faltas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Falta falta = db.Faltas.Find(id);
            db.Faltas.Remove(falta);
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
