using FlexHealthDomain.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Models
{
    public class Resultado
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string ExameURL { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public User Usuario { get; set; }

        [Required]
        public int MedicoId { get; set; }

        [Required]
        public User Medico { get; set; }

        [Required]
        public int AgendamentoId { get; set; }

        [Required]
        public Agendamento Agendamento { get; set; }

        [Required]
        public string Proposito { get; set; }

        [Required]
        public bool Visibilidade { get; set; }
    }
}
