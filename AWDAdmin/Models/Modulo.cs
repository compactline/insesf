using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{

    [Table("Modulos")]
    public class Modulo
    {

        public Modulo()
        {
            this.Disciplinas = new List<Disciplina>();
        }
        [Key]
        public Int32 Id { get; set; }
        
        [Required]
        public string Nome { get; set; }

        [ForeignKey("Curso")]
        public Int32 Curso_Id { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual ICollection<Disciplina> Disciplinas { get; set; }
    }
}