using Microsoft.EntityFrameworkCore;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Repositories
{
    public class RoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lay ra Role theo id
        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }

        // Lay ra danh sach Role
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        // Them moi Role
        public async Task<Role> CreateAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        // Cap nhat Role
        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        // Xoa Role
        public async Task<bool> DeleteAsync(Role role)
        {
           _context.Roles.Update(role);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
