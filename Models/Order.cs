using System;
using System.ComponentModel.DataAnnotations;

namespace Zadanie_5.Models
{
    public class Order
    {
        [Required]
        [Range(0,int.MaxValue)]
        public int IdOrder { get; set; }
        
        [Required]
        [Range(0,int.MaxValue)]
        public int IdProduct { get; set; }
        
        [Required]
        [Range(1,int.MaxValue)]
        public int Amount { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public DateTime FulfilledAt { get; set; }

        public override string ToString()
        {
            return "IdOrder: " + IdOrder + "\n" +
                   "IdProduct: " + IdProduct + "\n" +
                   "Amount: " + Amount + "\n" +
                   "CreatedAt: " + CreatedAt + "\n" +
                   "FulfilledAt: " + FulfilledAt + "\n";
        }
    }
}