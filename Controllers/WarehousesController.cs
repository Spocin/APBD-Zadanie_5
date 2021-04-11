using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zadanie_5.Models;
using Zadanie_5.Services;

namespace Zadanie_5.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IDbService _service;

        public WarehousesController(IDbService service)
        {
            _service = service;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> RegisterProduct([FromBody] Product product)
        {
            switch (await _service.CheckIfProductIdExists(product.IdProduct))
            {
                case false:
                    return StatusCode(400, "Such Product Id doesn't exists");
            }

            switch (await _service.CheckIfWarehouseIdExists(product.IdWarehouse))
            {
                case false:
                    return StatusCode(400, "Such Warehouse Id doesn't exists");
            }
            
            var idOrder = _service.CheckIfOrderExists(product.IdProduct, product.Amount).Result;

            switch (idOrder)
            {
                case (-1):
                    return StatusCode(400, "Such Order Id doesn't exists");
            }

            switch (await _service.CheckIfOrderNotRealised(idOrder))
            {
                case false:
                    return StatusCode(400, "Such Order is already realised");
            }
            
            switch (await _service.CheckIfOrderExistsInProduct_Warehouse(idOrder))
            {
                case true:
                    return StatusCode(400, "Such Order already exists in Product_Warehouse");
            };

            _service.UpdateFullfilledAt(idOrder);
            return Ok(_service.InsertIntoProduct_Warehouse(idOrder, product));
        }
    }
}