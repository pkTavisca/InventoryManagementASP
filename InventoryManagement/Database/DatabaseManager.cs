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

        public void InsertNewProduct(string productName, int productQuantity, int productPrice)
        {
            string sql = $"INSERT INTO Products(Name, Quantity, Price) VALUES ('{productName}', {productQuantity}, {productPrice}) ";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void UpdateProduct(int productId, int productQuantity)
        {
            string sql = $"UPDATE Products SET Quantity = {productQuantity} WHERE Id = {productId}";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void DeleteProduct(int productId)
        {
            string sql = $"DELETE FROM Products WHERE Id = {productId}";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public void InsertInOrder(decimal price)
        {
            string sql = $"INSERT INTO Orders(DateTime, Name, Price) VALUES (CURRENT_TIMESTAMP, NULL, {price}) ";
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

        public void InsertInOrderDetails(int orderId, string itemId, int itemQuantity, decimal price)
        {
            string sql = $"INSERT INTO OrderDetails(OrderId, ProductId, Quantity, Price) VALUES ({orderId}, {itemId}, {itemQuantity}, {price}) ";
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
            using (var reader = command.ExecuteReader())
            {
                return Product.GenerateProduct(reader);
            }
        }
    }
}