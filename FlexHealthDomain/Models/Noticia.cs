using FlexHealthDomain.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Models
{
    public class Noticia
    {  
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Texto { get; set; }

        [Required]
        public string ImagemUrl { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; }

        [Required]
        public int EstabelecimentoId { get; set; }

        [Required]
        public User Estabelecimento { get; set; }
    }
}
