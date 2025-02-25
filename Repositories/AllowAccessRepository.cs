using Microsoft.EntityFrameworkCore;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Repositories
{
    public class AllowAccessRepository
    {
        private readonly AppDbContext _context;

        public AllowAccessRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lay ra AllowAccess 
        public async Task<List<AllowAccess>> GetAllowAccessesAsync()
        {
            return await _context.AllowAccesses.ToListAsync();
        }

        // Lay ra AllowAccess theo id
        public async Task<AllowAccess> GetByIdAsync(int id)
        {
            return await _context.AllowAccesses.FirstOrDefaultAsync(a => a.Id == id);
        }

        // Them moi AllowAccess
        public async Task<AllowAccess> CreateAsync(AllowAccess allowAccess)
        {
            await _context.AllowAccesses.AddAsync(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }

        // Cap nhat AllowAccess
        public async Task UpdateAsync(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Update(allowAccess);
            await _context.SaveChangesAsync();
        }

        // Xoa AllowAccess
        public async Task DeleteAsync(AllowAccess allowAccess)
        {
            _context.AllowAccesses.Remove(allowAccess);
            await _context.SaveChangesAsync();
        }
    }
}
