using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;
using SecureApiWithJwt.Repositories;
using SecureApiWithJwt.Services.IServices;

namespace SecureApiWithJwt.Services
{
    public class InternService : IInternService
    {
        private readonly InternRepository _internRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAllowAccessService _allowAccessService;

        public InternService(
            InternRepository internRepository,
            IHttpContextAccessor httpContextAccessor,
            IAllowAccessService allowAccessService)
        {
            _internRepository = internRepository;
            _httpContextAccessor = httpContextAccessor;
            _allowAccessService = allowAccessService;
        }

        // Lay thong tin UserId và RoleId tu token
        private (string UserId, string RoleId) GetUserInfoFromToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirst("UserId")?.Value;
            var roleId = user?.FindFirst("RoleId")?.Value;
            return (userId, roleId);
        }

        // Lay tat ca Intern
        public async Task<ApiResponse<List<InternResponse>>> GetInternsAsync()
        {
            var (userId, roleId) = GetUserInfoFromToken();
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId) || !int.TryParse(roleId, out int parsedRoleId))
            {
                return ApiResponse<List<InternResponse>>.Unauthorized();
            }

            var allowedColumnsResponse = await _allowAccessService.GetAllowedColumnsAsync(parsedRoleId, "Intern");

            if (allowedColumnsResponse.Code != 0 || string.IsNullOrEmpty(allowedColumnsResponse.Data))
            {
                return ApiResponse<List<InternResponse>>.Forbidden("You do not have permission to access the Intern table.");
            }

            var allowedColumnList = allowedColumnsResponse.Data.Split(',').Select(s => s.Trim()).ToHashSet();
            var interns = await _internRepository.GetInternsAsync();

            var filteredInterns = interns.Select(intern => FilterInternProperties(intern, allowedColumnList)).ToList();
            return ApiResponse<List<InternResponse>>.Success(filteredInterns);
        }

        // Lay Intern theo ID
        public async Task<ApiResponse<InternResponse>> GetInternByIdAsync(int id)
        {
            var (userId, roleId) = GetUserInfoFromToken();
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleId) || !int.TryParse(roleId, out int parsedRoleId))
            {
                return ApiResponse<InternResponse>.Unauthorized();
            }

            var allowedColumnsResponse = await _allowAccessService.GetAllowedColumnsAsync(parsedRoleId, "Intern");

            if (allowedColumnsResponse.Code != 0 || string.IsNullOrEmpty(allowedColumnsResponse.Data))
            {
                return ApiResponse<InternResponse>.Forbidden("You do not have permission to access the Intern table.");
            }

            var allowedColumnList = allowedColumnsResponse.Data.Split(',').Select(s => s.Trim()).ToHashSet();

            var intern = await _internRepository.GetInternByIdAsync(id);
            if (intern == null)
            {
                return ApiResponse<InternResponse>.NotFound();
            }

            var filteredIntern = FilterInternProperties(intern, allowedColumnList);
            return ApiResponse<InternResponse>.Success(filteredIntern);
        }

        // Loc cac thuoc tinh dua tren quyen truy cap
        private InternResponse FilterInternProperties(Intern intern, HashSet<string> allowedColumns)
        {
            var filteredIntern = new InternResponse();
            var internType = typeof(Intern);
            var responseType = typeof(InternResponse);

            foreach (var propertyName in allowedColumns)
            {
                var propertyInfo = internType.GetProperty(propertyName);
                var responseProperty = responseType.GetProperty(propertyName);

                if (propertyInfo != null && responseProperty != null)
                {
                    var value = propertyInfo.GetValue(intern);
                    if (value != null)
                    {
                        responseProperty.SetValue(filteredIntern, value);
                    }
                }
            }

            return filteredIntern;
        }

    }
}