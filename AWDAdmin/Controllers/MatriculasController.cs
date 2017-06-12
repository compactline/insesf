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
    public class MatriculasController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Matriculas
        public ActionResult Index()
        {

            ViewBag.IDCurso = new SelectList(db.Cursos, "ID", "Titulo");
            return View();
        }

        [HttpPost]
        public ActionResult GetDados(string NomeAluno, string IdCurso)
        {
            var matriculas = db.Matriculas.Include(m => m.Aluno).Include(m => m.Curso);

            if (!string.IsNullOrEmpty(NomeAluno))
            {
                matriculas = matriculas.Where(m => m.Aluno.Nome.Contains(NomeAluno));
            }

            var id = 0;
            if (!string.IsNullOrEmpty(IdCurso))
            {
                id = Int32.Parse(IdCurso);
                matriculas = matriculas.Where(m => m.IDCurso == id);
            }

            ViewBag.IDCurso = new SelectList(db.Cursos, "ID", "Titulo", id);
            return PartialView("_Table",matriculas.ToList());
        }
        // GET: Matriculas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // GET: Matriculas/Create
        public ActionResult Create()
        {
            ViewBag.IDAluno = new SelectList(db.Alunos, "ID", "Nome");
            ViewBag.IDCurso = new SelectList(db.Cursos, "ID", "Titulo");
            return View();
        }

        // POST: Matriculas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IDAluno,IDCurso,DataMatricula,Situacao,DataPago,IdTransacao")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.Matriculas.Add(matricula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDAluno = new SelectList(db.Alunos, "ID", "Nome", matricula.IDAluno);
            ViewBag.IDCurso = new SelectList(db.Cursos, "ID", "Titulo", matricula.IDCurso);
            return View(matricula);
        }

        // GET: Matriculas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAluno = new SelectList(db.Alunos, "ID", "Nome", matricula.IDAluno);
            ViewBag.IDCurso = new SelectList(db.Cursos, "ID", "Titulo", matricula.IDCurso);
            return View(matricula);
        }

        // POST: Matriculas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IDAluno,IDCurso,DataMatricula,Situacao,DataPago,IdTransacao")] Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matricula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDAluno = new SelectList(db.Alunos, "ID", "Nome", matricula.IDAluno);
            ViewBag.IDCurso = new SelectList(db.Cursos, "ID", "Titulo", matricula.IDCurso);
            return View(matricula);
        }

        // GET: Matriculas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matricula matricula = db.Matriculas.Find(id);
            if (matricula == null)
            {
                return HttpNotFound();
            }
            return View(matricula);
        }

        // POST: Matriculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matricula matricula = db.Matriculas.Find(id);
            db.Matriculas.Remove(matricula);
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
