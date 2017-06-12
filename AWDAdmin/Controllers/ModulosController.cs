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
    public class ModulosController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Modulos
        public ActionResult Index(int? curso)
        {
            var id = curso.HasValue ? curso.Value : 0;
            var moduloes = db.Moduloes.Where(m=>m.Curso_Id==id).Include(m => m.Curso);
            ViewBag.curso = db.Cursos.Find(id);
            return View(moduloes.ToList());
        }

        // GET: Modulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Moduloes.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // GET: Modulos/Create
        public ActionResult Create(int? curso)
        {
            var id = curso.HasValue ? curso.Value : 0;

            ViewBag.Curso = db.Cursos.Find(curso);
            return View();
        }

        // POST: Modulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Curso_Id")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Moduloes.Add(modulo);
                db.SaveChanges();
                ViewBag.Mensagem = "Dados salvos com sucesso";
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível salvar os dados.";
            }

            ViewBag.curso = db.Cursos.Find(modulo.Curso_Id);

            return View(modulo);
        }

        // GET: Modulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Moduloes.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Curso_Id = new SelectList(db.Cursos, "ID", "Codigo", modulo.Curso_Id);
            return View(modulo);
        }

        // POST: Modulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Curso_Id")] Modulo modulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Curso_Id = new SelectList(db.Cursos, "ID", "Codigo", modulo.Curso_Id);
            return View(modulo);
        }

        // GET: Modulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modulo modulo = db.Moduloes.Find(id);
            if (modulo == null)
            {
                return HttpNotFound();
            }
            return View(modulo);
        }

        // POST: Modulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modulo modulo = db.Moduloes.Find(id);
            db.Moduloes.Remove(modulo);
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
