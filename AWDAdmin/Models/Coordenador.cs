using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    [Table("Coordenadores")]
    public class Coordenador
    {
        [Key]
        public Int32 Id { get; set; }

        public string Curriculo { get; set; }

        [ForeignKey("Pessoa")]
        [Column("Pessoa_Id")]
        public Int32 PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}