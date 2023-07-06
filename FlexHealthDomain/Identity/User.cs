using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Identity
{
    public class User : IdentityUser<int>
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        [Required]
        public string Rg { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Required]
        public IEnumerable<UserRole> Acessos { get; set; }
    }
}
