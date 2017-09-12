using InventoryManagement.Models;
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

        public void InsertInOrder()
        {
            string sql = "INSERT INTO Orders(DateTime, Name) VALUES (CURRENT_TIMESTAMP, NULL) ";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void RemoveFromProductStore(string itemId, int cartItemQuantity)
        {
            int quantity = GetQuantityOfProduct(itemId);
            string sql = $"UPDATE Products SET Quantity = {quantity - cartItemQuantity} WHERE Id = {itemId}";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        private int GetQuantityOfProduct(string productId)
        {
            string sql = $"SELECT Quantity FROM Products WHERE Id = {productId}";
            SqlCommand command = new SqlCommand(sql, _connection);
            return (int)command.ExecuteScalar();
        }

        public void InsertInOrderDetails(int orderId, string itemId, int itemQuantity)
        {
            string sql = $"INSERT INTO OrderDetails(OrderId, ProductId, Quantity) VALUES ({orderId}, {itemId}, {itemQuantity}) ";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public int GetLastOrderId()
        {
            string sql = "SELECT MAX(Id) FROM Orders";
            SqlCommand command = new SqlCommand(sql, _connection);
            return (int)command.ExecuteScalar();
        }

        public void Disconnect()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }

        public List<Product> GetAllProducts()
        {
            string sql = "SELECT * FROM Products";
            SqlCommand command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();
            return Product.GenerateProduct(reader);
        }
    }
}