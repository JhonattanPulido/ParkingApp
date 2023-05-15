// Libraries
using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using Repositories.Interfaces;
using Utilitaries.DTO.Pager;
using System.Data;
using static Dapper.SqlMapper;

namespace Repositories
{
    /// <summary>
    /// Logs repository layer
    /// </summary>
    public class LogRepository : ILogRepository
    {
        // Vars
        private SqlConnection SqlConnection { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LogRepository(SqlConnection sqlConnection)
        {
            SqlConnection = sqlConnection;
        }

        // Methods

        /// <summary>
        /// Create parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        public async Task Create(Log log) =>
            await SqlConnection.QueryAsync(
                "[dbo].[CreateLog]",
                new
                {
                    ENTRY = log.Entry,
                    NUMBER_PLATE = log.Vehicle?.NumberPlate,
                    VEHICLE_TYPE_ID = log.Vehicle?.Type?.Id
                },
                commandType: CommandType.StoredProcedure
                );

        /// <summary>
        /// Get log by number plate
        /// </summary>
        /// <param name="numberPlate">Vehicle number plate</param>
        /// <returns>Log information</returns>
        public async Task<Log?> Get(string numberPlate) =>
            await SqlConnection.QuerySingleOrDefaultAsync<Log?>(
                "[dbo].[GetLog]",
                new
                {
                    NUMBER_PLATE = numberPlate,
                },
                commandType: CommandType.StoredProcedure
                );

        /// <summary>
        /// Get paginated logs
        /// </summary>
        /// <param name="pagerInput">Pager input data</param>
        /// <returns>Paginated logs</returns>
        public async Task<PagerOutputDTO<Log>> Get(PagerInputDTO pagerInput)
        {
            using GridReader gridReader = await SqlConnection.QueryMultipleAsync(
                "[dbo].[GetLogs]",
                new
                {
                    ENTRY = pagerInput.Entry,
                    DEPARTURE = pagerInput.Departure,
                    PAGE_INDEX = pagerInput.PageIndex,
                    ITEMS_COUNT = pagerInput.ItemsCount
                },
                commandType: CommandType.StoredProcedure
                );

            List<Log> logs = gridReader.Read<Log>().ToList();
            int totalItems = await gridReader.ReadSingleAsync<int>();
            PagerOutputDTO<Log> pagerOutput = new(logs, totalItems);

            return pagerOutput;
        }

        /// <summary>
        /// Update parking log
        /// </summary>
        /// <param name="log">Parking log data</param>
        /// <returns>Update status</returns>
        public async Task<string> Update(Log log) =>
            await SqlConnection.QueryFirstAsync<string>(
                "[dbo].[UpdateLog]",
                new
                {
                    NUMBER_PLATE = log.Vehicle?.NumberPlate,
                    DEPARTURE = log.Departure,
                    PRICE = log.Price,
                    TIME = log.Time,
                    BILL_DISCOUNT_NUMBER = log.BillDiscountNumber
                },
                commandType: CommandType.StoredProcedure
                );
    }
}
