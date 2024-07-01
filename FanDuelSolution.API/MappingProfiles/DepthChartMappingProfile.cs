using AutoMapper;

namespace FanDuelSolution.API.MappingProfiles
{
    public class DepthChartMappingProfile : Profile
    {
        public DepthChartMappingProfile()
        {
            CreateMap<Entities.DepthChart, Models.DepthChartDto>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.FullName()))
                .ForMember(dest => dest.PositionType, opt => opt.MapFrom(src => src.Position.PositionType));
        }
    }
}
