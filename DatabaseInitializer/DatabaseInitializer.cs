using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInitializer
{
    public class DatabaseInit
    {
        private readonly string _connection;
        public DatabaseInit (string _connection)
        {
            this._connection = _connection;
        }
        public async Task<bool> InitializeDatabaseAsync(string sqlFilePath)
        {
            try
            {
                string sql = await File.ReadAllTextAsync(sqlFilePath);
                using SqlConnection connection = new SqlConnection(_connection);
                SqlCommand command = new SqlCommand(sql, connection);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {
                //log error
                Console.WriteLine(ex.Message.ToString());
                return false;
            }
        }
    }
}
