using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class NoticiaDto
    {
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string ImagemUrl { get; set; }
        public IFormFile Imagem { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
