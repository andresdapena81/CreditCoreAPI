using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCore.Domain.Entities
{
    public class PlanPago
    {
        public int Id { get; set; }
        public int CreditoId { get; set; }
        public Credito Credito { get; set; }

        public int NumeroCuota { get; set; }
        public decimal CuotaMensual { get; set; }
        public decimal AbonoCapital { get; set; }
        public decimal AbonoInteres { get; set; }
        public decimal SaldoRestante { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }
}
