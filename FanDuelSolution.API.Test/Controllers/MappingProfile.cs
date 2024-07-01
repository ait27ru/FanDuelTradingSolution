using AutoMapper;

namespace FanDuelSolution.API.Test.Controllers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.DepthChart, Models.DepthChartDto>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.FullName()))
                .ForMember(dest => dest.PositionType, opt => opt.MapFrom(src => src.Position.PositionType));

            CreateMap<Entities.DepthChart, Models.PlayerDto>()
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Player.Number))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Player.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Player.LastName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Player.FullName()));
        }
    }
}
