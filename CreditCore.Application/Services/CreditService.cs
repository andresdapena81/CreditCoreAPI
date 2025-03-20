using CreditCore.Domain.Entities;
using CreditCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CreditCore.Application.DTOs;


namespace CreditCore.Application.Services
{
    public class CreditService
    {
        private readonly CreditDbContext _context;

        public CreditService(CreditDbContext context)
        {
            _context = context;
        }

        public async Task<List<CreditStatusDto>> ObtenerCreditosCliente(int clienteId)
        {
            var creditos = await _context.Creditos
                .Where(c => c.ClienteId == clienteId)
                .Include(c => c.PlanPagos)
                .ToListAsync();

            var resultado = new List<CreditStatusDto>();

            foreach (var credito in creditos)
            {
                var cuotasVencidas = credito.PlanPagos
                    .Where(p => !p.Pagado && p.FechaPago < DateTime.Now)
                    .ToList();

                int totalCuotasAtrasadas = cuotasVencidas.Count;
                int diasMora = totalCuotasAtrasadas > 0
                    ? (DateTime.Now - cuotasVencidas.Min(p => p.FechaPago)).Days
                    : 0;

                resultado.Add(new CreditStatusDto
                {
                    CodigoCredito = credito.NumeroCredito,
                    Estado = totalCuotasAtrasadas > 0 ? "En mora" : "Al día",
                    CuotasAtrasadas = totalCuotasAtrasadas,
                    DiasMora = diasMora
                });
            }

            return resultado;
        }
    }
}
