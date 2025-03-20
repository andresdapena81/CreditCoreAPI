using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCore.Domain.Entities
{
    public class PlanPago
    {
        public int Id { get; set; }
        public int CreditoId { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CuotaMensual { get; set; }
        public decimal AbonoCapital { get; set; }
        public decimal AbonoInteres { get; set; }
        public decimal SaldoRestante { get; set; }
        public DateTime FechaPago { get; set; }
        public bool Pagado { get; set; }

        // ✅ Agregar propiedad de navegación a Credito
        [ForeignKey("CreditoId")]
        public virtual Credito Credito { get; set; }
    }
}