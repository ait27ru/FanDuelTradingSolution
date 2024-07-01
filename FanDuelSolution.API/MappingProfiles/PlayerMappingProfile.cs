using AutoMapper;

namespace FanDuelSolution.API.MappingProfiles
{
    public class PlayerMappingProfile : Profile
    {
        public PlayerMappingProfile()
        {
            CreateMap<Entities.DepthChart, Models.PlayerDto>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Player.Number))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Player.FullName()));
        }
    }
}
