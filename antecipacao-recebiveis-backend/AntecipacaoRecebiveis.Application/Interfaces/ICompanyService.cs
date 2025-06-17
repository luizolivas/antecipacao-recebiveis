using AntecipacaoRecebiveis.Domain.Entities;


namespace AntecipacaoRecebiveis.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task<Company> CreateAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task DeleteAsync(int id);
        Task<decimal> GetCreditLimitByIdAsync(int id);
    }
}
