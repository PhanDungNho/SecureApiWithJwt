using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;
using SecureApiWithJwt.Repositories;
using SecureApiWithJwt.Services.IServices;

namespace SecureApiWithJwt.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(RoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // Lay ra danh sach Role
        public async Task<ApiResponse<List<RoleResponse>>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetRolesAsync();
            var rolesResponse = _mapper.Map<List<RoleResponse>>(roles);
           return ApiResponse<List<RoleResponse>>.Success(rolesResponse);
        }

        // Lay ra Role theo id
        public async Task<ApiResponse<RoleResponse>> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return ApiResponse<RoleResponse>.NotFound("Role not found");
            }
            var roleResponse = _mapper.Map<RoleResponse>(role);
            return ApiResponse<RoleResponse>.Success(roleResponse);
        }

        // Them moi Role
        public async Task<ApiResponse<RoleResponse>> CreateAsync(RoleRequest roleRequest)
        {
            var role = _mapper.Map<Role>(roleRequest);
            role.IsActive = true;
            var roleCreated = await _roleRepository.CreateAsync(role);
            var roleResponse = _mapper.Map<RoleResponse>(roleCreated);
            return ApiResponse<RoleResponse>.Success(roleResponse);
        }

        // Cap nhat Role
        public async Task<ApiResponse<RoleResponse>> UpdateAsync(int id, RoleRequest roleRequest)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return ApiResponse<RoleResponse>.NotFound("Role not found");

            _mapper.Map(roleRequest, role);
            await _roleRepository.UpdateAsync(role);

            var roleResponse = _mapper.Map<RoleResponse>(role);
            return ApiResponse<RoleResponse>.Success(roleResponse);
        }

        // Xoa Role 
        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null) return ApiResponse<bool>.NotFound("Role not found");
            
            role.IsActive = !role.IsActive;
            bool result = await _roleRepository.DeleteAsync(role);

            if(!result) return ApiResponse<bool>.Error(new Dictionary<string, string[]> { { "Delete", new string[] { "Error deleting role" } } });
            return ApiResponse<bool>.Success(result);
        }
    }
}
