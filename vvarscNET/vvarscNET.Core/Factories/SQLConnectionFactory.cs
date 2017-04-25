using System;
using System.Data;
using System.Data.SqlClient;

namespace vvarscNET.Core.Factories
{
    public class SQLConnectionFactory
    {
        private string _connectionString;

        public SQLConnectionFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            _connectionString = connectionString;
        }
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
