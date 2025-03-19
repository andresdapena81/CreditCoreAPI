using Microsoft.AspNetCore.Mvc;
using CreditCore.Domain.Entities;
using CreditCore.Infrastructure.Persistence;
using CreditCore.Application.Services;

namespace CreditCore.API.Controllers
{
    [Route("api/creditos")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly CreditDbContext _context;

        public CreditoController(CreditDbContext context)
        {
            _context = context;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearCredito([FromBody] Credito credito)
        {
            var cliente = await _context.Clientes.FindAsync(credito.ClienteId);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado.");
            }

            // Generar número de crédito único
            credito.NumeroCredito = $"CR-{Guid.NewGuid().ToString().Substring(0, 8)}";

            _context.Creditos.Add(credito);
            await _context.SaveChangesAsync();

            // Generar Plan de Pagos
            var planPagos = PaymentProjectionService.CalcularPlanPagos(credito.Monto, credito.PlazoMeses, credito.TasaInteres)
                .Select(p => new PlanPago
                {
                    CreditoId = credito.Id,
                    NumeroCuota = p.NumeroCuota,
                    CuotaMensual = p.CuotaMensual,
                    AbonoCapital = p.AbonoCapital,
                    AbonoInteres = p.AbonoInteres,
                    SaldoRestante = p.SaldoRestante,
                    FechaPago = DateTime.UtcNow.AddMonths(p.NumeroCuota)
                }).ToList();

            _context.PlanPagos.AddRange(planPagos);
            await _context.SaveChangesAsync();

            return Ok(new { credito, planPagos });
        }
    }
}
