using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Tests
{
    public class CreditLimitCalculatorTest
    {

        private readonly ICreditLimitCalculator _creditLimitCalculator;

        public CreditLimitCalculatorTest()
        {
            _creditLimitCalculator = new CreditLimitCalculator();
        }

        [Theory]
        [InlineData(9000, Sector.PRODUCAO, 0)]            
        [InlineData(15000, Sector.PRODUCAO, 7500)]       
        [InlineData(50000, Sector.SERVICO, 25000)]       
        [InlineData(75000, Sector.SERVICO, 41250)]       
        [InlineData(75000, Sector.PRODUCAO, 45000)]       
        [InlineData(120000, Sector.SERVICO, 72000)]      
        [InlineData(120000, Sector.PRODUCAO, 78000)]    
        public void CalculateCreditLimit_CalculatesAccordingToMonthlyBilling(decimal monthlyBilling, Sector sector, decimal expectedLimit)
        {
            decimal result = _creditLimitCalculator.CalculateCreditLimit(monthlyBilling, sector);

            Assert.Equal(expectedLimit, result);
        }
    }
}
