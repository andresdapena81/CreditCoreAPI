using Microsoft.AspNetCore.Mvc;
using CreditCore.Domain.Entities;
using CreditCore.Infrastructure.Persistence;

namespace CreditCore.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly CreditDbContext _context;

        public ClienteController(CreditDbContext context)
        {
            _context = context;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            if (_context.Clientes.Any(c => c.Documento == cliente.Documento))
            {
                return BadRequest("El cliente ya está registrado.");
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok(cliente);
        }
    }
}