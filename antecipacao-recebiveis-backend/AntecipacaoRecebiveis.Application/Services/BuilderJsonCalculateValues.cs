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
    public class BuilderJsonCalculateValues : IBuilderJsonCalculateValues
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICompanyRepository _companyRepository;

        public BuilderJsonCalculateValues(
            ICartItemRepository cartItemRepository,
            ICompanyRepository companyRepository)
        {
            _cartItemRepository = cartItemRepository;
            _companyRepository = companyRepository;
        }

        public async Task<object> GetDetailedCalculationJsonAsync(int companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            var cartItems = await _cartItemRepository.GetAllByCompanyIdAsync(companyId);

            var nfes = CalculateNfes(cartItems);
            var totalBruto = CalculateGrossTotal(nfes);
            var totalLiquido = CalculateNetTotal(nfes);

            return new
            {
                empresa = company?.Name,
                cnpj = company?.Cnpj,
                limite = company?.CreditLimit ?? 0,
                notas_fiscais = nfes.Select(n => new
                {
                    numero = n.Numero,
                    valor_bruto = n.ValorBruto,
                    valor_liquido = n.ValorLiquido
                }),
                total_bruto = Math.Round(totalBruto, 2),
                total_liquido = Math.Round(totalLiquido, 2)
            };
        }

        public decimal CalculateGrossTotal(IEnumerable<CalculatedNfe> nfes)
        {
            return nfes.Sum(n => n.ValorBruto);
        }

        public decimal CalculateNetTotal(IEnumerable<CalculatedNfe> nfes)
        {
            return nfes.Sum(n => n.ValorLiquido);
        }

        public List<CalculatedNfe> CalculateNfes(IEnumerable<CartItem> cartItems)
        {
            const double rate = 0.0465;
            var today = DateTime.Now;

            return cartItems
                .Where(i => i.Nfe != null)
                .Select(i =>
                {
                    var nfe = i.Nfe!;
                    var days = Math.Max(0, (nfe.ExpirationDate - today).Days) + 1;
                    var valorLiquido = (double)nfe.Value / Math.Pow(1 + rate, days / 30.0);

                    return new CalculatedNfe {
                        Numero = nfe.Number,
                        ValorBruto = nfe.Value,
                        ValorLiquido = Math.Round((decimal)valorLiquido, 2)
                    };
                })
                .ToList();
        }

    }
}
