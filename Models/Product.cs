using System;
using System.ComponentModel.DataAnnotations;

namespace Zadanie_5.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product Id is required", AllowEmptyStrings = false)]
        public int IdProduct { get; set; }
        
        [Required(ErrorMessage = "Warehouse Id is required", AllowEmptyStrings = false)]
        public int IdWarehouse { get; set; }
        
        [Required(ErrorMessage = "Amount is required", AllowEmptyStrings = false)]
        [Range(1,int.MaxValue)]
        public int Amount { get; set; }
        
        [Required(ErrorMessage = "Timestamp is required", AllowEmptyStrings = false)]
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return "IdProduct: " + IdProduct + "\n" +
                   "IdWarehouse: " + IdWarehouse + "\n" +
                   "Amount: " + Amount + "\n" +
                   "CreatedAt: " + CreatedAt + "\n";
        }
    }
}