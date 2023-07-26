using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class ArquivoDto
    {
        public int Id { get; set; }
        public IFormFile Arquivo { get; set; }
        public string URL { get; set; }
        public int UsuarioId { get; set; }
        public int MedicoId { get; set; }
        public int AgendamentoId { get; set; }
        public string Proposito { get; set; }
        public string TipoExame { get; set; }
        public bool Visibilidade { get; set; }
    }
}
