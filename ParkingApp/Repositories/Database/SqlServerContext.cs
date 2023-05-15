// Libraries

using Microsoft.Data.SqlClient;

namespace Repositories.Database
{
    /// <summary>
    /// SQL Server
    /// </summary>
    public class SqlServerContext
    {
        // Methods

        /// <summary>
        /// Get SQL Server connection
        /// </summary>
        /// <returns>Prepared client connection</returns>
        public static SqlConnection GetConnection() =>
            new(connectionString: Environment.GetEnvironmentVariable("SqlServerConnectionString"));
    }
}
