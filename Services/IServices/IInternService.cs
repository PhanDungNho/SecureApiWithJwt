using SecureApiWithJwt.DTOs.Responses;

namespace SecureApiWithJwt.Services.IServices
{
    public interface IInternService
    {
        Task<ApiResponse<List<InternResponse>>> GetInternsAsync();
        Task<ApiResponse<InternResponse>> GetInternByIdAsync(int id);
    }
}
