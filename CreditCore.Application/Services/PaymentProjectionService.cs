using CreditCore.Shared.Models;

namespace CreditCore.Application.Services
{
    public class PaymentProjectionService
    {
        public List<PaymentProjectionDto> CalcularPlanPagos(decimal monto, int plazoMeses, double tasaInteres)
        {
            var planPagos = new List<PaymentProjectionDto>();

            // Convertir tasa de interés anual a mensual
            double tasaMensual = tasaInteres / 100 / 12;

            // Calcular cuota mensual fija (Fórmula de amortización francesa)
            decimal cuotaMensual = monto * (decimal)(tasaMensual / (1 - Math.Pow(1 + tasaMensual, -plazoMeses)));

            decimal saldo = monto;

            for (int i = 1; i <= plazoMeses; i++)
            {
                decimal interes = saldo * (decimal)tasaMensual;
                decimal abonoCapital = cuotaMensual - interes;
                saldo -= abonoCapital;

                planPagos.Add(new PaymentProjectionDto
                {
                    NumeroCuota = i,
                    CuotaMensual = Math.Round(cuotaMensual, 2),
                    AbonoCapital = Math.Round(abonoCapital, 2),
                    AbonoInteres = Math.Round(interes, 2),
                    SaldoRestante = Math.Round(saldo, 2)
                });
            }

            return planPagos;
        }
    }
}
