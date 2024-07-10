using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8ConsoleApp.CustomAdoDotNetService
{
    public class AdoDotNetService
    {
        public readonly string _connStr = "Data Source=.;Database=testDb;User ID=sa;Password=sasa@123";
        public async Task<List<T>> QueryAsync<T>(string query, SqlParameter[]? parameters = null, SqlTransaction? transaction = null)
        {
            SqlConnection conn = GetConnection();
            await conn.OpenAsync();
            SqlCommand cmd = new(query, conn, transaction);
            if (parameters is not null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            SqlDataAdapter adapter = new(cmd);
            DataTable dt = new();
            adapter.Fill(dt);
            await conn.CloseAsync();

            string jsonStr = JsonConvert.SerializeObject(dt);
            var lst = JsonConvert.DeserializeObject<List<T>>(jsonStr)!;

            return lst;
        }

        public async Task<DataTable> QueryFirstOrDefaultAsync(string query, SqlParameter[]? parameters = null, SqlTransaction? transaction = null)
        {
            SqlConnection conn = GetConnection();
            await conn.OpenAsync();
            SqlCommand cmd = new(query, conn, transaction);
            if (parameters is not null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            SqlDataAdapter adapter = new(cmd);
            DataTable dt = new();
            adapter.Fill(dt);
            await conn.CloseAsync();

            return dt;
        }

        public async Task<int> ExecuteAsync(string query, SqlParameter[]? parameters = null, SqlTransaction? transaction = null)
        {
            SqlConnection conn = GetConnection();
            await conn.OpenAsync();
            SqlCommand cmd = new(query, conn, transaction);
            if (parameters is not null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            int result = await cmd.ExecuteNonQueryAsync();
            await conn.CloseAsync();

            return result;
        }

        private SqlConnection GetConnection() => new(_connStr);
    }
}
