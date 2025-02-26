using AutoMapper;
using SecureApiWithJwt.DTOs.Requests;
using SecureApiWithJwt.DTOs.Responses;
using SecureApiWithJwt.Models;

namespace SecureApiWithJwt.Mappers
{
    public class InternMapper : Profile
    {
        public InternMapper()
        {
            CreateMap<Intern, InternResponse>();
            CreateMap<InternRequest, Intern>();
        }
    }
}
