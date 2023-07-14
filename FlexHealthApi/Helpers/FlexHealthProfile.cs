using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;

namespace FlexHealthApi.Helpers
{
    public class FlexHealthProfile : Profile
    {
        public FlexHealthProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap(); 
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<Agendamento, AgendamentoDto>().ReverseMap();
            CreateMap<Agendamento, AgendaDto>().ReverseMap();
        }
    }
}
