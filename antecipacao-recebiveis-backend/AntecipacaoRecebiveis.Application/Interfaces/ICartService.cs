using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(int companyId, int nfeId);
        Task RemoveFromCartAsync(int companyId, int nfeId);
        Task<List<Nfe>> GetCartItemsAsync(int companyId);
        Task<decimal> GetCartTotalAsync(int companyId);
    }
}
