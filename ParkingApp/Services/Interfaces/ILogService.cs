// Libraries
using Utilitaries.DTO;
using Utilitaries.DTO.Pager;

namespace Services.Interfaces
{
    /// <summary>
    /// Log service interface
    /// </summary>
    public interface ILogService
    {
        // Methods

        /// <summary>
        /// Create parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        public Task Create(LogDTO log);

        /// <summary>
        /// Get paginated logs
        /// </summary>
        /// <param name="pagerInput">Pager input data</param>
        /// <returns>Paginated logs</returns>
        public Task<PagerOutputDTO<LogDTO>> Get(PagerInputDTO pagerInput);

        /// <summary>
        /// Update parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        /// <returns>Update status</returns>
        public Task Update(string id, DateTime departure, string? billDiscountNumber);
    }
}
