using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;

namespace SecureApiWithJwt.Services.IServices
{
    public interface IAllowAccessService
    {
        Task<ApiResponse<List<AllowAccessResponse>>> GetAllowAccessesAsync();
        Task<ApiResponse<AllowAccessResponse>> GetByIdAsync(int id);
        Task<ApiResponse<AllowAccessResponse>> CreateAsync(AllowAccessRequest allowAccessRequest);
        Task<ApiResponse<AllowAccessResponse>> UpdateAsync(int id, AllowAccessRequest allowAccessRequest);
        Task<ApiResponse<AllowAccessResponse>> DeleteAsync(int id);
        Task<ApiResponse<string>> GetAllowedColumnsAsync(int roleId, string tableName);
    }
}
