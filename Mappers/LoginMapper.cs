using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Mappers
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<User, LoginResponse>();
        }
    }
}
