using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Site.Context
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



        public System.Data.Entity.DbSet<Site.Models.Curso> Cursos { get; set; }
        public System.Data.Entity.DbSet<Site.Models.Aluno> Alunos { get; set; }
        public System.Data.Entity.DbSet<Site.Models.Matricula> Matriculas { get; set; }

    }
}