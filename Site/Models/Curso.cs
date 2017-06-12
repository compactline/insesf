using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Site.Models
{
    [Table("Cursos")]
    public class Curso
    {
        [Key]
        public int ID { get; set; }
        public string Codigo{ get; set; }
        public string Capa { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Ementa { get; set; }
        public string CargaHoraria { get; set; }
        public string NomeProfessor { get; set; }
        public string NomeCoordenador { get; set; }
        public int Vagas{ get; set; }
        public DateTime DataInicio{ get; set; }
        public DateTime DataConclusao{ get; set; }
        public string Horario { get; set; }

        public DateTime InicioInscricao { get; set; }
        public DateTime FimInscricao { get; set; }

        public string Local{ get; set; }
        public string Investimento { get; set; }

        public Decimal Valor { get; set; }

    }
}