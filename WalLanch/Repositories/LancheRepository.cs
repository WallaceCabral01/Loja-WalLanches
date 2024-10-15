using Microsoft.EntityFrameworkCore;
using WalLanches.Context;
using WalLanches.Models;
using WalLanches.Repositories.Interfaces;

namespace WalLanches.Repositories

{
    public class LancheRepository : ILancheRepository
    {

        private readonly AppDbContext _context;



        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p =>
        p.IsLanchePreferido).Include(c => c.Categoria);

        public Lanche GetLancheByld(int lancheId) => _context.Lanches.FirstOrDefault(p => p.LancheId == lancheId);
    }

}
