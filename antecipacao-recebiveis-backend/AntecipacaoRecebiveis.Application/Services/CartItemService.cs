using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Exceptions;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using AntecipacaoRecebiveis.Infrastructure.Repositories;
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
        private readonly ICompanyService _companyService;
        private readonly INfeService _nfeService;
        private readonly IBuilderJsonCalculateValues _builderJson;


        public CartItemService(ICartItemRepository repository, ICompanyService companyService, INfeService nfeService, IBuilderJsonCalculateValues builderJson )
        {
            _repository = repository;
            _companyService = companyService;
            _nfeService = nfeService;
            _builderJson = builderJson;
        }

        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            var nfe = await _nfeService.GetByIdAsync(cartItem.NfeId);

            if (nfe == null)
                throw new Exception("Nota fiscal não encontrada.");

            bool underLimit = await IsValueWithinCreditLimit(cartItem.CompanyId, nfe.Value);

            if (!underLimit)
            {
                throw new CreditLimitExceededException("O valor ultrapassa o limite de crédito da empresa.");
            }

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

        public async Task<decimal> CalculateTotalBrutoAsync(int companyId)
        {
            return await _repository.GetTotalValorBrutoByCompanyidAsync(companyId);
        }

        public async Task<bool> IsValueWithinCreditLimit(int companyId, decimal newValue)
        {
            var totalValorBruto = await CalculateTotalBrutoAsync(companyId);
            var creditLimit = await _companyService.GetCreditLimitByIdAsync(companyId);

            return (totalValorBruto + newValue) <= creditLimit;

        }

        public async Task<object> GetDetailedCalculationAsync(int companyId)
        {
            return await _builderJson.GetDetailedCalculationJsonAsync(companyId);

        }
    }
}
