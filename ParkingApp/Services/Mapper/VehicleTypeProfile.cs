// Libraries
using AutoMapper;
using Utilitaries.DTO;

namespace Models.Mapper
{
    /// <summary>
    /// Vehicle type profile class
    /// </summary>
    public class VehicleTypeProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public VehicleTypeProfile()
        {
            // Vehicle type DTO -> Vehicle type mapping definition
            CreateMap<VehicleTypeDTO, VehicleType>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.Name, o => o.MapFrom(x => x.Name))
                .ForMember(d => d.Cost, o => o.MapFrom(x => x.Cost))
                .ReverseMap();
        }
    }
}
