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
    public class CursosController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Cursos
        public ActionResult Index()
        {
            return View(db.Cursos.ToList());
        }


        public ActionResult List()
        {
            return PartialView("_List",db.Cursos.ToList());
        }

        // GET: Cursos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codigo,Capa,Titulo,Descricao,Ementa,CargaHoraria,NomeProfessor,NomeCoordenador,Vagas,DataInicio,DataConclusao,Horario,InicioInscricao,FimInscricao,Local,Investimento,Valor")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                curso.Capa = curso.Capa == null ? "" : curso.Capa;
                db.Cursos.Add(curso);
                db.SaveChanges();
                ViewBag.Mensagem = "Dados salvos com sucesso";
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível salvar os dados.";
            }

            return View(curso);
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codigo,Capa,Titulo,Descricao,Ementa,CargaHoraria,NomeProfessor,NomeCoordenador,Vagas,DataInicio,DataConclusao,Horario,InicioInscricao,FimInscricao,Local,Investimento,Valor")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                curso.Capa = curso.Capa == null ? "" : curso.Capa;
                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Mensagem = "Dados salvos com sucesso";
            }
            else
            {
                ViewBag.Mensagem = "Não foi possível salvar os dados.";
            }
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
            db.SaveChanges();
            return Content("Registro removido com sucesso.");
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
