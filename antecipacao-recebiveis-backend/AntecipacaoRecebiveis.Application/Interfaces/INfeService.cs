

namespace AntecipacaoRecebiveis.Application.Interfaces
{
    public interface INfeService
    {
        Task<IEnumerable<Nfe>> GetAllAsync();
        Task<Nfe> GetByIdAsync(int id);
        Task<Nfe> CreateAsync(Nfe nfe);
        Task<Nfe> UpdateAsync(Nfe nfe);
        Task DeleteAsync(int id);
        Task<IEnumerable<Nfe>> GetByCompanyIdAsync(int idCompany);
        Task<decimal> CalculateNetTotalAsync(int companyId);
    }
}
