using AutoMapper;

namespace Xpand.API.Profiles
{
    public class PlanetProfile: Profile
    {
        public PlanetProfile()
        {
            CreateMap<Domain.Models.Planet, DTOs.Planet>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Image.Bytes));
        }
    }
}
