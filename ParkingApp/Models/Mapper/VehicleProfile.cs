// Libraries
using AutoMapper;
using Utilitaries.DTO;

namespace Models.Mapper
{
    /// <summary>
    /// Vehicle profile
    /// </summary>
    public class VehicleProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VehicleProfile()
        {
            // Vehicle DTO -> Vehicle mapping definition (with reverse mapping)
            CreateMap<VehicleDTO, Vehicle>()
                .ForMember(d => d.NumberPlate, o => o.MapFrom(x => x.NumberPlate)).ReverseMap()
                .ForMember(d => d.Type, o => o.MapFrom(x => x.Type)).ReverseMap();
        }
    }
}
