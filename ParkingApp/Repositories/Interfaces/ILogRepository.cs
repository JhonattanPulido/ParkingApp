// Libraries
using Models;
using Utilitaries.DTO.Pager;

namespace Repositories.Interfaces
{
    /// <summary>
    /// Logs repository interface
    /// </summary>
    public interface ILogRepository
    {
        // Methods

        /// <summary>
        /// Create parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        public Task Create(Log log);

        /// <summary>
        /// Get paginated logs
        /// </summary>
        /// <param name="pagerInput">Pager input data</param>
        /// <returns>Paginated logs</returns>
        public Task<PagerOutputDTO<Log>> Get(PagerInputDTO pagerInput);

        /// <summary>
        /// Update parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        /// <returns>Update status</returns>
        public Task<string> Update(Log log);
    }
}
