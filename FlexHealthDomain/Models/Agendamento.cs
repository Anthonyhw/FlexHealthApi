using FlexHealthDomain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Models
{
    public class Agendamento
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime DataConsulta { get; set; }

        public DateTime DataMarcacao { get; set; }

        [Required]
        public string Status { get; set; }

        public int? UsuarioId { get; set; }

        public User Usuario { get; set; }

        [Required]
        public int MedicoId { get; set; }

        [Required]
        public User Medico { get; set; }

        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Valor { get; set; }
        [Required]
        public string Especialidade { get; set; }
    }
}
