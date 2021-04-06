using System;

namespace Zadanie_5.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public int IdWarehouse { get; set; }
        public int Amount { get; set; }
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