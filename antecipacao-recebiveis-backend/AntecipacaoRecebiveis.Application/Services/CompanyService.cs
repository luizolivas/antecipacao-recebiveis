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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICreditLimitCalculator _creditLimitCalculator;

        public CompanyService(ICompanyRepository companyRepository, ICreditLimitCalculator creditLimitCalculator)
        {
            _companyRepository = companyRepository;
            _creditLimitCalculator = creditLimitCalculator;
        }

        public async Task<IEnumerable<Company>> GetAllAsync() => await _companyRepository.GetAllAsync();

        public async Task<Company> GetByIdAsync(int id) => await _companyRepository.GetByIdAsync(id);

        public async Task<Company> CreateAsync(Company company)
        {
            company.CreditLimit = _creditLimitCalculator.CalculateCreditLimit(company.MonthlyBiling, company.Sector);
            return await _companyRepository.CreateAsync(company);
        }

        public async Task<Company> UpdateAsync(Company company) => await _companyRepository.UpdateAsync(company);

        public async Task DeleteAsync(int id) => await _companyRepository.DeleteAsync(id);
    }
}
