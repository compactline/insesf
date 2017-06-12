using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    [Table("Disciplinas")]
    public class Disciplina
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [ForeignKey("Modulo")]
        public Int32 Modulo_Id { get; set; }

        public virtual Modulo Modulo { get; set; }

    }
}