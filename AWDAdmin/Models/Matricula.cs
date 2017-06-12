using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
       
    [Table("Matriculas")]
    public class Matricula
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Aluno")]
        public int IDAluno { get; set; }

        [ForeignKey("Curso")]
        public int IDCurso { get; set; }
        public DateTime DataMatricula { get; set; }
        public string Situacao { get; set; }
        public DateTime DataPago { get; set; }
        public string IdTransacao { get; set; }

        public virtual Aluno Aluno { get;set;}
        public virtual Curso Curso { get; set; }
    }
}