using AutoMapper;

namespace Xpand.API.Profiles
{
    public class PlanetProfile: Profile
    {
        public PlanetProfile()
        {
            CreateMap<Domain.Models.Planet, DTOs.Planet>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Image.Bytes))
                .ForMember(d => d.Description, o =>
                {
                    o.Condition(s => s.DescriptionId.HasValue && s.Description != null);
                    o.MapFrom(s => s.Description!.Text);
                })
                .ForMember(d => d.DescriptionAuthor, o =>
                {
                    o.Condition(s => s.DescriptionId.HasValue && s.Description != null);
                    o.MapFrom(s => s.Description!.Author);
                });
        }
    }
}
