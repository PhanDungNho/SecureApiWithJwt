using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;

namespace SecureApiWithJwt.Services.IServices
{
    public interface IRoleService
    {
        Task<ApiResponse<List<RoleResponse>>> GetRolesAsync();
        Task<ApiResponse<RoleResponse>> GetByIdAsync(int id);
        Task<ApiResponse<RoleResponse>> CreateAsync(RoleRequest roleRequest);
        Task<ApiResponse<RoleResponse>> UpdateAsync(int id, RoleRequest roleRequest);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
