using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
        }   
    }
}
