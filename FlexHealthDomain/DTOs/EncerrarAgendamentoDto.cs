using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class EncerrarAgendamentoDto
    {
        public ArquivoDto[]? Arquivos { get; set; }
        public string AgendamentoId { get; set; }
    }
}
