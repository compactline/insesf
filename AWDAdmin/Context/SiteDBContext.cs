using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AWDAdmin.Context
{
    public class SiteDBContext:DbContext
    {
        public SiteDBContext()
            : base("DefaultConnection")
        { }

       // protected override void OnModelCreating(DbModelBuilder modelBuilder)
       // {
            //base.OnModelCreating(modelBuilder);
           // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
       // }



        public System.Data.Entity.DbSet<AWDAdmin.Models.Curso> Cursos { get; set; }
        public System.Data.Entity.DbSet<AWDAdmin.Models.Aluno> Alunos { get; set; }
        public System.Data.Entity.DbSet<AWDAdmin.Models.Matricula> Matriculas { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Pessoa> Pessoas { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Professor> Professores { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Coordenador> Coordenadores{ get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Area> Areas { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Noticia> Noticias { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Modulo> Moduloes { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Disciplina> Disciplinas { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Falta> Faltas { get; set; }

        public System.Data.Entity.DbSet<AWDAdmin.Models.Nota> Notas { get; set; }
    }
}