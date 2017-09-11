using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public static List<Product> GenerateProduct(SqlDataReader reader)
        {
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString(),
                    Price = decimal.Parse(reader[2].ToString()),
                    Quantity = int.Parse(reader[3].ToString())
                };
                products.Add(product);
            }
            return products;
        }
    }
}