using Microsoft.AspNetCore.Mvc;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;

namespace SecureApiWithJwt.Services.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserResponse>>> GetUsersAsync();
        Task<ApiResponse<UserResponse>> GetUserByIdAsync(int id);
        Task<ApiResponse<UserResponse>> CreateUserAsync(UserRequest userRequest);
        Task<ApiResponse<UserResponse>> UpdateUserAsync(int id, UserRequest userRequest);
        Task<ApiResponse<UserResponse>> DeleteUserAsync(int id);
    }
}
