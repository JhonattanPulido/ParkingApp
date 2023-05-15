// Libraries
using AutoMapper;
using Models;
using Repositories.Interfaces;
using Services.Interfaces;
using Utilitaries.DTO;
using Utilitaries.DTO.Pager;

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

            // DONE;
            return pagerData;
        }

        /// <summary>
        /// Update parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        /// <returns>Update status</returns>
        public async Task Update(LogDTO log)
        {
            
        }
    }
}
