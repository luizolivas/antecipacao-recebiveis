using AntecipacaoRecebiveis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItem>> GetAllByCompanyIdAsync(int companyId);
        Task<CartItem> AddAsync(CartItem cartItem);
        Task RemoveAsync(int cartItemId);
        Task<decimal> GetTotalCartValueAsync(int companyId);
    }
}
