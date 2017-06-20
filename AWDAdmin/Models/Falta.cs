using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    [Table("Faltas")]
    public class Falta
    {
        [Key]
        public Int32 Id { get; set; }
        public DateTime Data { get; set; }
        [ForeignKey("Disciplina")]
        public Int32 Disciplina_Id { get; set; }
        [ForeignKey("Aluno")]
        public Int32 Aluno_Id { get; set; }

        public virtual Disciplina Disciplina { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}