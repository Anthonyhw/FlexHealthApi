using FlexHealthDomain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class AgendaDto
    {
        public string Id { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public string DataConsulta { get; set; }
        public string DataMarcacao { get; set; }
        public int EstabelecimentoId { get; set; }
        public int MedicoId { get; set; }
        public int UsarioId{ get; set; }
        public string Especialidade { get; set; }
        public string Valor { get; set; }
    }
}
