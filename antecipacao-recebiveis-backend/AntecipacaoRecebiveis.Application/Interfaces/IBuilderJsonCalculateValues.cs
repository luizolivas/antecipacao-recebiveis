using AntecipacaoRecebiveis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Interfaces
{
    public interface IBuilderJsonCalculateValues
    {
        Task<object> GetDetailedCalculationJsonAsync(int companyId);
        List<CalculatedNfe> CalculateNfes(IEnumerable<CartItem> cartItems);
        decimal CalculateGrossTotal(IEnumerable<CalculatedNfe> nfes);
        decimal CalculateNetTotal(IEnumerable<CalculatedNfe> nfes);
    }
}
