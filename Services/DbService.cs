using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Zadanie_5.Models;

namespace Zadanie_5.Services
{
    public class DbService : IDbService
    {
        private readonly IConfiguration _configuration;
        
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private async Task<bool> ExecuteSql(SqlCommand command)
        {
            await using var connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            command.Connection = connection;
            
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            return await reader.ReadAsync();
        }
        
        public async Task<bool> CheckIfProductIdExists(int idProduct)
        {
            const string sqlCommand = "SELECT 1 FROM Product WHERE IdProduct=@idProduct";
            await using var  command = new SqlCommand(sqlCommand);
            command.Parameters.AddWithValue("@idProduct", idProduct);

            return ExecuteSql(command).Result;
        }

        public async Task<bool> CheckIfWarehouseIdExists(int idWarehouse)
        {
            const string sqlCommand = "SELECT 1 FROM Warehouse WHERE IdWarehouse=@idWarehouse";
            await using var command = new SqlCommand(sqlCommand);
            command.Parameters.AddWithValue("@idWarehouse", idWarehouse);

            return ExecuteSql(command).Result;
        }

        public async Task<Order> CheckIfOrderExists(int idProduct, int amount)
        {
            const string sqlCommand = "SELECT * FROM [Order] WHERE IdProduct=@idProduct AND Amount=@amount";
            await using var command = new SqlCommand(sqlCommand);
            command.Parameters.AddWithValue("@idProduct", idProduct);
            command.Parameters.AddWithValue("@amount", amount);

            await using var connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            command.Connection = connection;
            
            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            switch (reader.ReadAsync().Result)
            {
                case true:
                    return new Order
                    {
                        IdOrder = int.Parse(reader["IdOrder"].ToString()),
                        IdProduct = int.Parse(reader["IdProduct"].ToString()),
                        Amount = int.Parse(reader["Amount"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString())
                    };

                case false:
                    return null;
            }
        }

        public async Task<bool> CheckIfOrderNotRealised(int idOrder)
        {
            const string sqlCommand = "SELECT 1 FROM [Order] WHERE idOrder=@idOrder AND FulfilledAt IS NULL";
            await using var command = new SqlCommand(sqlCommand);
            command.Parameters.AddWithValue("@idOrder", idOrder);

            return ExecuteSql(command).Result;
        }

        public async Task<bool> CheckIfOrderExistsInProduct_Warehouse(int idOrder)
        {
            var sqlCommand = "SELECT 1 FROM Product_Warehouse WHERE IdOrder=@idOrder";
            await using var command = new SqlCommand(sqlCommand);
            command.Parameters.AddWithValue("@idOrder", idOrder);

            var tmp = ExecuteSql(command).Result;
            return tmp;
        }

        public async void UpdateFullfilledAt(int idOrder)
        {
            await using var connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            
            var sqlCommand = "UPDATE [Order] SET FulfilledAt=@dateTime WHERE IdOrder=@idOrder";
            await using var command = new SqlCommand(sqlCommand,connection);
            command.Parameters.AddWithValue("@dateTime", DateTime.Now);
            command.Parameters.AddWithValue("@idOrder", idOrder);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task<int> InsertIntoProduct_Warehouse(Product product, Order order)
        {
            await using var connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));

            var sqlCommand =
                "INSERT INTO Product_Warehouse (IdWarehouse,IdProduct,IdOrder,Amount,Price,CreatedAt) VALUES (@idWarehouse,@idProduct,@idOrder,@amount,@price,@createdAt); SELECT SCOPE_IDENTITY()";

            await using var command = new SqlCommand(sqlCommand, connection);

            command.Parameters.AddWithValue("@idWarehouse", product.IdWarehouse);
            command.Parameters.AddWithValue("@idProduct", product.IdProduct);
            command.Parameters.AddWithValue("@idOrder", order.IdOrder);
            command.Parameters.AddWithValue("@amount", order.Amount);
            command.Parameters.AddWithValue("@price", 1);
            command.Parameters.AddWithValue("@createdAt", DateTime.Now);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();

            await reader.ReadAsync();

            return decimal.ToInt32(reader.GetDecimal(0));
        }
    }
}