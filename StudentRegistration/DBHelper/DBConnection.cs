using Microsoft.Data.SqlClient;

namespace StudentRegistration.DBHelper
{
    public class DBConnection : IDBConnection
    {
        private readonly IConfiguration _configuration;
        public DBConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
