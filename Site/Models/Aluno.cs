using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Site.Models
{
    
    [Table("Alunos")]
 
    public class Aluno
    {
        [Key]
        public int ID { set; get; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Preencha o seu nome.")]
        public string Nome { get; set; }
        [Required]

        [Display(Name = "Nome Genitora")]
        public string NomeGenitora { get; set; }
        [Required]
        [Display(Name = "Data de Nascimento")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

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
        public string Telefone { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string RG { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public string Senha { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Cadastro")]

        public DateTime DataCadastro { get; set; }

    }
}