using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCore.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; } // Cédula o NIT
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        public List<Credito> Creditos { get; set; } = new List<Credito>();
    }
}
