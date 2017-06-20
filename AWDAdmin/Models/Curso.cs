using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AWDAdmin.Models
{
    [Table("Cursos")]
    public class Curso
    {
        public Curso()
        {
            this.Capa = "";
            this.Modulos = new List<Modulo>();
        }
        [Key]
        public int ID { get; set; }
        [Display(Name = "Código")]
        [Required]
        public string Codigo{ get; set; }
        public string Capa { get; set; }
        [Display(Name = "Título")]
        [Required]
        public string Titulo { get; set; }
        [Required]
        [MinLength(150)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required]
        public string Ementa { get; set; }
        [Display(Name = "Carga Horária")]
        [Required]
        public string CargaHoraria { get; set; }

        [Display(Name = "Professor")]
        public string NomeProfessor { get; set; }
        [Display(Name = "Coordenador")]
        public string NomeCoordenador { get; set; }
        [Required]
        public int Vagas{ get; set; }
        [Required]
        [Display(Name = "Data Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInicio{ get; set; }

        [Required]
        [Display(Name = "Data Fim")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataConclusao{ get; set; }

        [Required]
        public string Horario { get; set; }

        [Display(Name = "Início das Inscrições")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime InicioInscricao { get; set; }
        [Required]
        [Display(Name = "Fim das Inscrições")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FimInscricao { get; set; }
        [Required]
        public string Local{ get; set; }
        public string Investimento { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Valor")]
        public Decimal Valor { get; set; }

        [ForeignKey("Area")]
        public System.Nullable<Int32> Area_Id { get; set; }

        public virtual Area Area { get; set; }

        [ForeignKey("Coordenador")]
        public System.Nullable<Int32> Coordenador_Id { get; set; }

        public virtual Coordenador Coordenador { get; set; }

        [ForeignKey("Professor")]
        public System.Nullable<Int32> Professor_Id { get; set; }

        public virtual Professor Professor  { get; set; }


        public ICollection<Modulo> Modulos { get; set; }
    }
}