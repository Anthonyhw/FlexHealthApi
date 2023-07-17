using FlexHealthDomain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class AgendaPorEstabelecimentoDto
    {
        public User Estabelecimento { get; set; }
        public List<AgendaDto> Agenda { get; set; }
    }
}
