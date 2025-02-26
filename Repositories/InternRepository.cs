using Microsoft.EntityFrameworkCore;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Repositories
{
    public class InternRepository
    {
        private readonly AppDbContext _context;

        public InternRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lay ra tat ca Intern
        public async Task<List<Intern>> GetInternsAsync()
        {
            return await _context.Interns.ToListAsync();
        }

        // Lay ra Intern theo Id
        public async Task<Intern> GetInternByIdAsync(int id)
        {
            return await _context.Interns.FindAsync(id);
        }
    }
}
