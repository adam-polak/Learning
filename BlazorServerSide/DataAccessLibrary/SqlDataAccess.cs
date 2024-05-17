using System.Data;

namespace DataAccessLibrary
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAccess(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters) 
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using(IDbConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
            {

            }

            return null;
        }
    }
}