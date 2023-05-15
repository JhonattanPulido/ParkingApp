// Libraries
using AutoMapper;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Text.RegularExpressions;
using Utilitaries.DTO;
using Utilitaries.DTO.Pager;
using Utilitaries.Enums;
using Utilitaries.Exceptions;

namespace Services
{
    /// <summary>
    /// Log service layer
    /// </summary>
    public class LogService : ILogService
    {
        // Vars
        private IMapper Mapper { get; }
        private ILogRepository LogRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogService(IMapper mapper, ILogRepository logRepository)
        {
            Mapper = mapper;
            LogRepository = logRepository;
        }

        // Methods

        /// <summary>
        /// Create parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        public async Task Create(LogDTO log)
        {
            // Verify number plate by vehicle type
            Match match = log.Vehicle!.Type!.Id switch
            {
                (byte)VehicleTypesEnum.Bicycle => Regex.Match(log.Vehicle.NumberPlate!, "[0-9]{6}"),
                (byte)VehicleTypesEnum.Motorcycle => Regex.Match(log.Vehicle.NumberPlate!, "[A-Z]{3}[0-9]{2}[A-Z]{1}"),
                (byte)VehicleTypesEnum.Car => Regex.Match(log.Vehicle.NumberPlate!, "[A-Z]{3}[0-9]{3}"),
                _ => throw new CustomBadRequestException("The vehicle type is not correct")
            };

            // Verify if regular expression is OK
            if (!match.Success)
                throw new CustomBadRequestException("The number plate is not correct for the vehicle type");                

            // Map log DTO to Log entity
            Log logData = Mapper.Map<Log>(log);

            // Create parking log 
            await LogRepository.Create(logData);
        }

        /// <summary>
        /// Get paginated logs
        /// </summary>
        /// <param name="pagerInput">Pager input data</param>
        /// <returns>Paginated logs</returns>
        public async Task<PagerOutputDTO<LogDTO>> Get(PagerInputDTO pagerInput)
        {
            // Get paginated parking logs
            PagerOutputDTO<Log> pager = await LogRepository.Get(pagerInput);

            // Map Log items to Log DTO
            List<LogDTO> logs = Mapper.Map<List<LogDTO>>(pager.Items);

            // Set pager data
            PagerOutputDTO<LogDTO> pagerData = new(logs, pager.TotalItems, pagerInput.ItemsCount);
            
            return pagerData;
        }

        /// <summary>
        /// Update parking log
        /// </summary>
        /// <param name="numberPlate">Vehicle number plate</param>
        /// <param name="departure">Vehicle departure date and time</param>
        /// <param name="billDiscountNumber">Supermarket bill discount number</param>
        public async Task Update(string numberPlate, DateTime departure, string? billDiscountNumber)
        {
            // Get parking log data
            Log? logData = await LogRepository.Get(numberPlate) ?? throw new CustomNotFoundException("Parking log not found");

            // Map log to LogDTO
            LogDTO log = Mapper.Map<LogDTO>(logData);

            // Verify entry and departure dates and times
            if (log.Entry is null || log.Entry > departure)
                throw new CustomBadRequestException("Entry date is greater than departure date");

            // Subtract departure and entry date
            TimeSpan dateDiff = departure.Subtract(log.Entry.Value);

            // Set spend minutes
            double totalMinutes = Math.Ceiling(dateDiff.TotalMinutes);

            // Calculate price (Minutes * Vehicle type cost)
            float price = (float)(totalMinutes * log.Vehicle!.Type!.Cost!);

            // If bill discount number is not null apply discount
            if (billDiscountNumber is not null)
                price -= (float)(price * 0.3);

            // Set log data to update
            logData.Vehicle = Mapper.Map<Vehicle>(log.Vehicle);
            logData.Departure = departure;
            logData.Price = price;
            logData.BillDiscountNumber = billDiscountNumber;
            logData.Time = GetSpentTime(ref dateDiff);

            // Update parking log
            string status = await LogRepository.Update(logData);

            // Verify response status code
            if (status.Equals("01"))
                throw new CustomNotFoundException("Vehicle not found");
            else if (status.Equals("02"))
                throw new CustomNotFoundException("Parking log not found");
        }

        private static string GetSpentTime(ref TimeSpan time)
        {
            string result = $"{time.Hours}h {time.Minutes}m";

            if (time.Days > 0)
                result = $"{time.Days}d " + result;

            return result;
        }
    }
}
