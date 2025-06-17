using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _repository;

        public CartItemService(ICartItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            return await _repository.AddAsync(cartItem);
        }

        public async Task<IEnumerable<CartItem>> GetAllByCompanyIdAsync(int companyId)
        {
            return await _repository.GetAllByCompanyIdAsync(companyId);
        }

        public async Task<decimal> GetTotalCartValueAsync(int companyId)
        {
            var items = await _repository.GetAllByCompanyIdAsync(companyId);
            throw new NotImplementedException(); 
        }

        public async Task RemoveAsync(int cartItemId)
        {
            await _repository.RemoveAsync(cartItemId);
        }
    }
}
