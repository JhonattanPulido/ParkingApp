// Libraries
using AutoMapper;
using Newtonsoft.Json;
using Utilitaries.DTO;

namespace Models.Mapper
{
    /// <summary>
    /// Log mapping profile
    /// </summary>
    public class LogProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LogProfile()
        {
            // Log DTO -> Log mapping definition
            CreateMap<LogDTO, Log>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.Entry, o => o.MapFrom(x => x.Entry))
                .ForMember(d => d.Departure, o => o.MapFrom(x => x.Departure))
                .ForMember(d => d.Price, o => o.MapFrom(x => x.Price))
                .ForMember(d => d.Time, o => o.MapFrom(x => x.Time))
                .ForMember(d => d.BillDiscountNumber, o => o.MapFrom(x => x.BillDiscountNumber))
                .ForMember(d => d.Vehicle, o => o.MapFrom(x => x.Vehicle));

            // Log -> Log DTO mapping definition
            CreateMap<Log, LogDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.Entry, o => o.MapFrom(x => x.Entry))
                .ForMember(d => d.Departure, o => o.MapFrom(x => x.Departure))
                .ForMember(d => d.Price, o => o.MapFrom(x => x.Price))
                .ForMember(d => d.Time, o => o.MapFrom(x => x.Time))
                .ForMember(d => d.BillDiscountNumber, o => o.MapFrom(x => x.BillDiscountNumber))
                .ForMember(d => d.Vehicle, o => o.MapFrom(x => JsonConvert.DeserializeObject<VehicleDTO?>(x.VehicleJSON ?? "")));
        }
    }
}
