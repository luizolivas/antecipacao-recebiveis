using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Infrastructure.Data;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;

        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<IEnumerable<CartItem>> GetAllByCompanyIdAsync(int companyId)
        {
            return await _context.CartItems
                .Include(ci => ci.Nfe)
                .Where(ci => ci.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task RemoveAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetTotalCartValueAsync(int companyId)
        {
            return await _context.CartItems
                .Include(ci => ci.Nfe)
                .Where(ci => ci.CompanyId == companyId)
                .SumAsync(ci => ci.Nfe != null ? (decimal?)ci.Nfe.Value : 0) ?? 0;
        }
    }
}
