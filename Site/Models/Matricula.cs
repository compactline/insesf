using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Site.Models
{
       
    [Table("Matriculas")]
    public class Matricula
    {
        [Key]
        public int ID { get; set; }
        public int IDAluno { get; set; }
        public int IDCurso { get; set; }
        public DateTime DataMatricula { get; set; }
        public string Situacao { get; set; }
        public DateTime DataPago { get; set; }
        public string IdTransacao { get; set; }
    }
}