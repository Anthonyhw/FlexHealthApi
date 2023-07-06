using FlexHealthDomain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Models
{
    public class Exame
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime DataExame { get; set; }

        public DateTime DataMarcacao { get; set; }

        [Required]
        public string Status { get; set; }

        public int UsuarioId { get; set; }

        public User Usuario { get; set; }

        public int MedicoId { get; set; }

        public User Medico { get; set; }
    }
}
