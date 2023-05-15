// Libraries
using AutoMapper;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using Utilitaries.DTO;
using Utilitaries.DTO.Pager;
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
        /// <param name="log">Parking log data</param>
        /// <returns>Update status</returns>
        public async Task Update(string id, DateTime departure, string? billDiscountNumber)
        {
            // Get parking log data
            Log? logData = await LogRepository.Get(id);

            // Verify if log exists
            if (logData is null)
                throw new CustomNotFoundException("Parking log not found");

            // Map log to LogDTO
            LogDTO log = Mapper.Map<LogDTO>(logData);

            // Verify entry and departure dates and times
            if (log.Entry is null || log.Entry > departure)
                throw new CustomBadRequestException("Entry date is greater than departure date");

            // Subtract departure and entry date
            TimeSpan dateDiff = departure.Subtract(log.Entry.Value);

            // Calculate price (Minutes * Vehicle type cost)
            float price = (float)(dateDiff.Minutes * logData!.Vehicle!.Type!.Cost!);

            // If bill discount number is not null apply discount
            if (billDiscountNumber is not null)
                price -= (float)(price * 0.3);

            // Set log data to update
            logData.Departure = departure;
            logData.Price = price;
            logData.BillDiscountNumber = billDiscountNumber;

            // Update parking log
            string status = await LogRepository.Update(logData);
        }
    }
}
