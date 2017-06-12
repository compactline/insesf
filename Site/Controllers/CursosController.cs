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
    public class CursosController : Controller
    {
        private SiteDBContext db = new SiteDBContext();

        // GET: Cursos
        public async Task<ActionResult> Index()
        {
            return View(await db.Cursos.ToListAsync());
        }



        public async Task<ActionResult>  MaisCursos()
        {
            ViewBag.Title = "AWD Cursos - Mais Cursos";
            var lista = await db.Cursos.ToListAsync();
            return View(lista);
        }


        // GET: Cursos/Details/5
        public async Task<ActionResult> Detalhe(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = await db.Cursos.Where(c => c.Codigo.Equals(id)).FirstOrDefaultAsync();
            if (curso == null)
            {
                return HttpNotFound();
            }
            int totalInscritos = db.Matriculas.Where(m => !m.Situacao.Equals("C") && m.IDCurso==curso.ID).Count();
            ViewBag.NumeroInscritos = totalInscritos;
            return View(curso);
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
