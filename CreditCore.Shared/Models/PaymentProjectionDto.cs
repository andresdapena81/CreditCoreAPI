using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCore.Shared.Models
{
    public class PaymentProjectionDto
    {
        public int NumeroCuota { get; set; }
        public decimal CuotaMensual { get; set; }
        public decimal AbonoCapital { get; set; }
        public decimal AbonoInteres { get; set; }
        public decimal SaldoRestante { get; set; }
    }
}