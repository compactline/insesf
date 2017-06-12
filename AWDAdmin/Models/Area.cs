using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    
    [Table("Areas")]
    public class Area
    {

        [Key]
        public Int32 Id { get; set; }
        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Nome { get; set; }
        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string Cor { get; set; }
    }
}