using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using AntecipacaoRecebiveis.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Services
{
    public class NfeService : INfeService
    {
        private readonly INfeRepository _nfeRepository;
        private readonly ICartItemService _cartItemService;

        public NfeService(INfeRepository nfeRepository, ICartItemService cartItemService)
        {
            _nfeRepository = nfeRepository;
            _cartItemService = cartItemService;
        }

        public async Task<IEnumerable<Nfe>> GetAllAsync() => await _nfeRepository.GetAllAsync();

        public async Task<Nfe> GetByIdAsync(int id) => await _nfeRepository.GetByIdAsync(id);

        public async Task<Nfe> CreateAsync(Nfe nfe) => await _nfeRepository.CreateAsync(nfe);

        public async Task<Nfe> UpdateAsync(Nfe nfe) => await _nfeRepository.UpdateAsync(nfe);

        public async Task DeleteAsync(int id) => await _nfeRepository.DeleteAsync(id);

        public async Task<IEnumerable<Nfe>> GetByCompanyIdAsync(int idCompany) => await _nfeRepository.GetByCompanyIdAsync(idCompany);

        public async Task<decimal> CalculateNetTotalAsync(int companyId)
        {
            var cartItems = await _cartItemService.GetAllByCompanyIdAsync(companyId);
            var today = DateTime.Now;
            const double rate = 0.0465;

            double total = 0;

            foreach (var item in cartItems)
            {
                var invoice = item.Nfe;
                if (invoice != null)
                {
                    var days = Math.Max(0, (invoice.ExpirationDate - today).Days) + 1;
                    var discount = (double)invoice.Value / Math.Pow(1 + rate, days / 30.0);
                    total += discount;
                }
            }

            return (decimal)total;
        }

    }
}
