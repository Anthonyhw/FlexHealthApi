
using FlexHealthDomain.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlexHealthDomain.Models
{
    public class Estabelecimento
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cnpj { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Tipo { get; set; }

        public virtual IEnumerable<User> Medicos { get; set; }
    }
}
