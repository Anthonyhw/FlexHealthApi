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
        public string Exame { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public User Usuario { get; set; }
    }
}
