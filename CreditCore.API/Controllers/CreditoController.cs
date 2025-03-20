using Microsoft.AspNetCore.Mvc;
using CreditCore.Domain.Entities;
using CreditCore.Infrastructure.Persistence;
using CreditCore.Application.Services;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CreditCore.API.Controllers
{
    [Route("api/creditos")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly CreditDbContext _context;
        private readonly PaymentProjectionService _paymentProjectionService;

        public CreditoController(CreditDbContext context, PaymentProjectionService paymentProjectionService)
        {
            _context = context;
            _paymentProjectionService = paymentProjectionService;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearCredito([FromBody] Credito credito)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
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

                // Asegurar que el Id se haya generado
                await _context.Entry(credito).GetDatabaseValuesAsync();

                // Generar Plan de Pagos
                var planPagos = _paymentProjectionService.CalcularPlanPagos(credito.Monto, credito.PlazoMeses, credito.TasaInteres)
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

                // Confirmar la transacción
                await transaction.CommitAsync();

                return Ok(new { credito, planPagos });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error al crear el crédito", error = ex.Message });
            }
        }
    }
}