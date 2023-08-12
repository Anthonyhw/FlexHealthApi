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
            CreateMap<Agendamento, AgendaDto>()
                .ForMember( a => a.Usuario, opt => opt.MapFrom(src => src.Usuario))
                .ForMember(a => a.Medico, opt => opt.MapFrom(src => src.Medico)).ReverseMap();
            CreateMap<Prescricao, ArquivoDto>().ReverseMap();
            CreateMap<Noticia, NoticiaDto>().ReverseMap();
        }
    }
}
