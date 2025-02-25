using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Repositories;
using SecureApiWithJwt.Services.IServices;
using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecureApiWithJwt.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly IJwtService _iJwtService;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper, IJwtService iJwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _iJwtService = iJwtService;
        }

        // Lay tat ca users
        public async Task<ApiResponse<List<UserResponse>>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return ApiResponse<List<UserResponse>>.Success(userResponses);
        }

        // Lay user theo id
        public async Task<ApiResponse<UserResponse>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserResponse>.NotFound("User not found");
            }
            var userResponse = _mapper.Map<UserResponse>(user);
            return ApiResponse<UserResponse>.Success(userResponse);
        }

        // Tao user
        public async Task<ApiResponse<UserResponse>> CreateUserAsync(UserRequest userRequest)
        {
            var user = _mapper.Map<User>(userRequest);
            var userCreated = await _userRepository.CreateAsync(user);
            var userResponse = _mapper.Map<UserResponse>(userCreated);
            return ApiResponse<UserResponse>.Success(userResponse);
        }

        // Cap nhat user
        public async Task<ApiResponse<UserResponse>> UpdateUserAsync(int id, UserRequest userRequest)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return ApiResponse<UserResponse>.NotFound("User not found");

            if (userRequest == null) return ApiResponse<UserResponse>.Error(new Dictionary<string, string[]> { { "Update", new string[] { "Invalid user data" } } });

            _mapper.Map(userRequest, user);
            try
            {
                var userUpdated = await _userRepository.UpdateAsync(user);
                var userResponse = _mapper.Map<UserResponse>(userUpdated);
                return ApiResponse<UserResponse>.Success(userResponse);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserResponse>.Error(new Dictionary<string, string[]> { { "Update", new string[] { "Error updating user", ex.Message } } });
            }
        }

        // Xoa user
        public async Task<ApiResponse<UserResponse>> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return ApiResponse<UserResponse>.NotFound("User not found");
            try
            {
                await _userRepository.DeleteAsync(user);
                var userResponse = _mapper.Map<UserResponse>(user);
                return ApiResponse<UserResponse>.Success(userResponse);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserResponse>.Error(new Dictionary<string, string[]> { { "Delete", new string[] { "Error deleting user", ex.Message } } });
            }
        }

        // Dang nhap
        public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.LoginAsync(loginRequest.Username);

            if (user == null || user.Password != loginRequest.Password) // Hash mật khẩu nếu cần
            {
                return ApiResponse<LoginResponse>.NotFound("Invalid username or password");
            }

            // Cap token cho user
            var token = _iJwtService.GenerateToken(user);

            var userResponse = _mapper.Map<LoginResponse>(user);
            userResponse.Token = token;

            return ApiResponse<LoginResponse>.Success(userResponse);
        }
    }
}
