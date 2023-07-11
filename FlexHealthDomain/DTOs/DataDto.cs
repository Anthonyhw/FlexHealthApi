using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.DTOs
{
    public class DataDto
    {
        public DateTime Dia { get; set; }
        public HorarioDto[] Horarios { get; set; }
    }
}
