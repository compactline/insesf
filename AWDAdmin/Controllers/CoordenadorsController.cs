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
    public class CoordenadorsController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Coordenadors
        public ActionResult Index()
        {
            var coordenadores = db.Coordenadores.Include(c => c.Pessoa);
            return View(coordenadores.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordenador coordenador = db.Coordenadores.Where(c=>c.Pessoa.Id==id.Value).FirstOrDefault();
            if (coordenador == null)
            {
                coordenador = new Coordenador();
                coordenador.Pessoa = db.Pessoas.Find(id);
                coordenador.PessoaId = id.Value;
                
            }
            return View(coordenador);
        }

        // POST: Coordenadors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Curriculo,PessoaId")] Coordenador coordenador)
        {

            Coordenador coord = db.Coordenadores.Find(coordenador.Id);
            
            if ( coord == null ){
                db.Coordenadores.Add(coordenador);
                db.SaveChanges();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    coord.Curriculo = coordenador.Curriculo;
                    db.Entry(coord).State = EntityState.Modified;
                    db.Entry(coord).Property("PessoaId").IsModified = false;
                    db.SaveChanges();
                }
            }

            return View(coordenador);
        }

        // GET: Coordenadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordenador coordenador = db.Coordenadores.Find(id);
            if (coordenador == null)
            {
                return HttpNotFound();
            }
            return View(coordenador);
        }

        // POST: Coordenadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coordenador coordenador = db.Coordenadores.Find(id);
            db.Coordenadores.Remove(coordenador);
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
