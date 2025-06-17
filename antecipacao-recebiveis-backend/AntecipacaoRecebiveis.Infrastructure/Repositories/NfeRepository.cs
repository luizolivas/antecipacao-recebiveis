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
    public class NfeRepository : INfeRepository
    {
        private readonly AppDbContext _context;

        public NfeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nfe>> GetAllAsync() => await _context.Nfes.Include(n => n.Company).ToListAsync();

        public async Task<Nfe> GetByIdAsync(int id) => await _context.Nfes.Include(n => n.Company).FirstOrDefaultAsync(n => n.Id == id);

        public async Task<Nfe> CreateAsync(Nfe nfe)
        {
            _context.Nfes.Add(nfe);
            await _context.SaveChangesAsync();
            return nfe;
        }

        public async Task<Nfe> UpdateAsync(Nfe nfe)
        {
            _context.Nfes.Update(nfe);
            await _context.SaveChangesAsync();
            return nfe;
        }

        public async Task DeleteAsync(int id)
        {
            var nfe = await _context.Nfes.FindAsync(id);
            if (nfe != null)
            {
                _context.Nfes.Remove(nfe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Nfe>> GetByCompanyIdAsync(int id)
        {
            return await _context.Nfes
                .Include(n => n.Company)
                .Where(n => n.Company.Id == id)
                .ToListAsync();
        }


    }
}
