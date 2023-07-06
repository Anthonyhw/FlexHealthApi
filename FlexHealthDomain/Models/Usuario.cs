using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexHealthDomain.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
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
        public string Telefone { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Senha { get; set; }

        [Required]
        public string Tipo { get; set; }
    }
}
