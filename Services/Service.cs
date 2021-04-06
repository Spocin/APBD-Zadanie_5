using System.Linq;
using Zadanie_5.Models;

namespace Zadanie_5.Services
{
    public class Service
    {
        public static bool CheckIfProductContainsNull(Product product)
        {
            return product.GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType == typeof(int))
                .Select(property => (int) property.GetValue(product))
                .All(val => val != 0);
        }
    }
}