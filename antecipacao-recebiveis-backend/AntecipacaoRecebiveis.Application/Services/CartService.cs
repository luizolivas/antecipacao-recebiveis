using AntecipacaoRecebiveis.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Services
{
    internal class CartService : ICartService
    {
        public Task AddToCartAsync(int companyId, int nfeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Nfe>> GetCartItemsAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetCartTotalAsync(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromCartAsync(int companyId, int nfeId)
        {
            throw new NotImplementedException();
        }
    }
}
