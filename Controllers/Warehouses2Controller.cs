using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Zadanie_5.Models;

namespace Zadanie_5.Controllers
{
    [Route("api/warehouses/tmp")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private IConfiguration _configuration;

        public Warehouses2Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProduct([FromBody] Product product)
        {
            await using var connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            await using var command = new SqlCommand("AddProductToWarehouse", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@IdProduct", product.IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
            command.Parameters.AddWithValue("@Amount", product.Amount);
            command.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return Ok();
        }
    }
}