using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Identity;

namespace FlexHealthApi.Helpers
{
    public class FlexHealthProfile : Profile
    {
        public FlexHealthProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap(); 
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
