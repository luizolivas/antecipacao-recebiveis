using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Interfaces
{
    public interface ICreditLimitCalculator
    {
        decimal CalculateCreditLimit(decimal monthlyBilling, Sector sector);
    }
}
