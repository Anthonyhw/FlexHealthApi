using FlexHealthDomain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexHealthDomain.Models
{
    public class Medico
    {

        public int Id { get; set; }


        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public int EstabelecimentoId { get; set; }

        [Required]
        public Estabelecimento Estabelecimento { get; set; }

        [Required]
        public string Crm { get; set; }

        [Required]
        public string Especialidade { get; set; }

        
    }
}
