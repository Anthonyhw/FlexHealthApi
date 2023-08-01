using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class AgendamentoParaUsuarioDto
    {
        public int UsuarioId { get; set; }
        public int AgendamentoId { get; set; }
        public string Pagamento { get; set; }
        public string StatusPagamento { get; set; }
    }
}
