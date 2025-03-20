using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCore.Application.DTOs
{
    public class CreditStatusDto
    {
        public string CodigoCredito { get; set; }
        public string Estado { get; set; } // "Al día" o "En mora"
        public int CuotasAtrasadas { get; set; }
        public int DiasMora { get; set; }
    }
}