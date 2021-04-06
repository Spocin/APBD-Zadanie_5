using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Zadanie_5.Models;
using Zadanie_5.Services;

namespace Zadanie_5.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesControler : ControllerBase
    {

        [HttpPost]
        public IActionResult RegisterWarehouse(string input)
        {
            var product = JsonSerializer.Deserialize<Product>(input);
            
            Console.WriteLine(product);

            switch (Service.CheckIfProductContainsNull(product))
            {
                case true:
                    //TODO 
                    break;
                case false:
                    return BadRequest("One field is empty");
            }

            return StatusCode(500);
        }
    }
}