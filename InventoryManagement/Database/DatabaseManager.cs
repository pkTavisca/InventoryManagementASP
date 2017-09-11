using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagement.Database
{
    public class DatabaseManager
    {
        private SqlConnection _connection;

        public bool Connect()
        {
            string dataSource = "TAVDESKRENT015";
            string database = "Inventory";
            string userId = "sa";
            string password = "test123!@#";

            string connectionString = $"Data Source = {dataSource}; Initial Catalog = {database}; User ID = {userId}; Password = {password}";
            _connection = new SqlConnection(connectionString);

            try
            {
                _connection.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Disconnect()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}