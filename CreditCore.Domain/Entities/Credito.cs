using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCore.Domain.Entities
{
    public class Credito
    {
        public int Id { get; set; }
        public string NumeroCredito { get; set; }
        public decimal Monto { get; set; }
        public double TasaInteres { get; set; }
        public int PlazoMeses { get; set; }
        public DateTime FechaDesembolso { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo";

        // Relación con Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public List<PlanPago> PlanPagos { get; set; } = new List<PlanPago>();
    }
}