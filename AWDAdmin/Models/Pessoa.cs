using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    [Table("Pessoas")]
    public class Pessoa
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string UF { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Celular { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string RG { get; set; }

    }
}