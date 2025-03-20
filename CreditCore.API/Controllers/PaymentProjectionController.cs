using Microsoft.AspNetCore.Mvc;
using CreditCore.Application.Services;
using CreditCore.Shared.Models;

namespace CreditCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProjectionController : ControllerBase
    {
        private readonly PaymentProjectionService _paymentProjectionService;

        public PaymentProjectionController(PaymentProjectionService paymentProjectionService)
        {
            _paymentProjectionService = paymentProjectionService;
        }

        [HttpGet("calculate")]
        public ActionResult<List<PaymentProjectionDto>> GetProjection(decimal monto, int plazoMeses, double tasaInteres)
        {
            if (monto <= 0 || plazoMeses <= 0 || tasaInteres <= 0)
                return BadRequest("Los valores deben ser mayores a cero");

            var planPagos = _paymentProjectionService.CalcularPlanPagos(monto, plazoMeses, tasaInteres);
            return Ok(planPagos);
        }
    }
}