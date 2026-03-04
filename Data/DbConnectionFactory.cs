
using System.Data;
using apiBukLitoprocess.conf;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;



namespace apiBukLitoprocess.Data;

public class DbConnectionFactory
{
 private readonly string _connectionString = string.Empty;

       public DbConnectionFactory(IConfiguration configuration)
        {
          _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
}
