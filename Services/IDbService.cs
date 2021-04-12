using System.Threading.Tasks;
using Zadanie_5.Models;

namespace Zadanie_5.Services
{
    public interface IDbService
    {
        public Task<bool> CheckIfProductIdExists(int idProduct);
        public Task<bool> CheckIfWarehouseIdExists(int idWarehouse);
        public Task<Order> CheckIfOrderExists(int idProduct, int amount);
        public Task<bool> CheckIfOrderNotRealised(int idOrder);
        public Task<bool> CheckIfOrderExistsInProduct_Warehouse(int idOrder);
        public void UpdateFullfilledAt(int idOrder);
        public Task<int> InsertIntoProduct_Warehouse(Product product, Order order);
    }
}