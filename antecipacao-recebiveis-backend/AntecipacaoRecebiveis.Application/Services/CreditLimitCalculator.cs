using AntecipacaoRecebiveis.Application.Interfaces;

namespace AntecipacaoRecebiveis.Application.Services
{
    public class CreditLimitCalculator : ICreditLimitCalculator
    {
        public decimal CalculateCreditLimit(decimal monthlyBilling, Sector sector)
        {
            if(monthlyBilling > 10000 && monthlyBilling <= 50000)
            {
                return monthlyBilling * 0.5m;
            }

            return sector switch {
                Sector.SERVICO when monthlyBilling >= 50001 && monthlyBilling <= 100000 => monthlyBilling * 0.55m,
                Sector.PRODUCAO when monthlyBilling >= 50001 && monthlyBilling <= 100000 => monthlyBilling * 0.6m,
                Sector.SERVICO when monthlyBilling > 100000 => monthlyBilling * 0.6m,
                Sector.PRODUCAO when monthlyBilling > 100000 => monthlyBilling * 0.65m,
                _ => 0m
            };
        }
    }
}
