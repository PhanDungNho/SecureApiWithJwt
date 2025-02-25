using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Mappers
{
    public class AllowAccessMapper : Profile
    {
        public AllowAccessMapper() 
        {
            CreateMap<AllowAccess, AllowAccessResponse>();
            CreateMap<AllowAccessRequest, AllowAccess>();
        }
    }
}
