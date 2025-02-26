using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;
using SecureApiWithJwt.Repositories;
using SecureApiWithJwt.Services.IServices;

namespace SecureApiWithJwt.Services
{
    public class AllowAccessService : IAllowAccessService
    {
        private readonly AllowAccessRepository _allowAccessRepository;
        private readonly IMapper _mapper;

        public AllowAccessService(AllowAccessRepository allowAccessRepository, IMapper mapper)
        {
            _allowAccessRepository = allowAccessRepository;
            _mapper = mapper;
        }

        // Lay ra danh sach AllowAccess
        public async Task<ApiResponse<List<AllowAccessResponse>>> GetAllowAccessesAsync()
        {
            var allowAccesses = await _allowAccessRepository.GetAllowAccessesAsync();
            var allowAccessesResponse = _mapper.Map<List<AllowAccessResponse>>(allowAccesses);
            return ApiResponse<List<AllowAccessResponse>>.Success(allowAccessesResponse);
        }

        // Lay ra AllowAccess theo RoleId va TableName
        public async Task<ApiResponse<string>> GetAllowedColumnsAsync(int roleId, string tableName)
        {
            var allowAccess = await _allowAccessRepository.GetByRoleAndTableAsync(roleId, tableName);

            if (allowAccess == null)
            {
                return ApiResponse<string>.NotFound();
            }

            return ApiResponse<string>.Success(allowAccess.AccessProperties ?? "");
        }


        // Lay ra AllowAccess theo id
        public async Task<ApiResponse<AllowAccessResponse>> GetByIdAsync(int id)
        {
            var allowAccess = await _allowAccessRepository.GetByIdAsync(id);
            if (allowAccess == null) return ApiResponse<AllowAccessResponse>.NotFound("AllowAccess not found");

            var allowAccessResponse = _mapper.Map<AllowAccessResponse>(allowAccess);
            return ApiResponse<AllowAccessResponse>.Success(allowAccessResponse);
        }

        // Them moi AllowAccess
        public async Task<ApiResponse<AllowAccessResponse>> CreateAsync(AllowAccessRequest allowAccessRequest)
        {
            var allowAccess = _mapper.Map<AllowAccess>(allowAccessRequest);
            var allowAccessCreated = await _allowAccessRepository.CreateAsync(allowAccess);
            var allowAccessResponse = _mapper.Map<AllowAccessResponse>(allowAccessCreated);
            return ApiResponse<AllowAccessResponse>.Success(allowAccessResponse);
        }

        // Cap nhat AllowAccess
        public async Task<ApiResponse<AllowAccessResponse>> UpdateAsync(int id, AllowAccessRequest allowAccessRequest)
        {
            var allowAccess = await _allowAccessRepository.GetByIdAsync(id);
            if (allowAccess == null) return ApiResponse<AllowAccessResponse>.NotFound("AllowAccess not found");

            allowAccess = _mapper.Map(allowAccessRequest, allowAccess);
            await _allowAccessRepository.UpdateAsync(allowAccess);

            var allowAccessResponse = _mapper.Map<AllowAccessResponse>(allowAccess);
            return ApiResponse<AllowAccessResponse>.Success(allowAccessResponse);
        }

        // Xoa AllowAccess
        public async Task<ApiResponse<AllowAccessResponse>> DeleteAsync(int id)
        {
            var allowAccess = await _allowAccessRepository.GetByIdAsync(id);
            if (allowAccess == null) return ApiResponse<AllowAccessResponse>.NotFound("AllowAccess not found");

            await _allowAccessRepository.DeleteAsync(allowAccess);
            var allowAccessResponse = _mapper.Map<AllowAccessResponse>(allowAccess);
            return ApiResponse<AllowAccessResponse>.Success(allowAccessResponse);
        }
    }
}
