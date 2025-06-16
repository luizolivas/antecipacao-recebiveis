using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
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

        public NfeService(INfeRepository nfeRepository)
        {
            _nfeRepository = nfeRepository;
        }

        public async Task<IEnumerable<Nfe>> GetAllAsync() => await _nfeRepository.GetAllAsync();

        public async Task<Nfe> GetByIdAsync(int id) => await _nfeRepository.GetByIdAsync(id);

        public async Task<Nfe> CreateAsync(Nfe nfe) => await _nfeRepository.CreateAsync(nfe);

        public async Task<Nfe> UpdateAsync(Nfe nfe) => await _nfeRepository.UpdateAsync(nfe);

        public async Task DeleteAsync(int id) => await _nfeRepository.DeleteAsync(id);

        public async Task<IEnumerable<Nfe>> GetByCompanyIdAsync(int idCompany) => await _nfeRepository.GetByCompanyIdAsync(idCompany);
    }
}
