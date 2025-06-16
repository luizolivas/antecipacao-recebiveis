using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Infrastructure.Interfaces
{
    public interface INfeRepository
    {
        Task<IEnumerable<Nfe>> GetAllAsync();
        Task<Nfe> GetByIdAsync(int id);
        Task<Nfe> CreateAsync(Nfe nfe);
        Task<Nfe> UpdateAsync(Nfe nfe);
        Task DeleteAsync(int id);
        Task<IEnumerable<Nfe>> GetByCompanyIdAsync(int id);
    }
}
