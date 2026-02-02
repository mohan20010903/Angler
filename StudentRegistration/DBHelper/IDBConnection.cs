using Microsoft.Data.SqlClient;

namespace StudentRegistration.DBHelper
{
    public interface IDBConnection
    {
        SqlConnection GetConnection();
    }
}
