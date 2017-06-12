using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    [Table("Noticias")]
    public class Noticia
    {
        [Key]
        public Int32 Id { get; set; }

        public string Titulo { get; set; }

        public string Resumo { get; set; }

        public string Texto {get;set;}

        public DateTime Cadastro { get; set; }

        public DateTime? Publicado { get; set; }

    }

}