using System.Threading.Tasks;
using Zadanie_5.Models;

namespace Zadanie_5.Services
{
    public interface IDbService
    {
        public Task<bool> CheckIfProductIdExists(int idProduct);
        public Task<bool> CheckIfWarehouseIdExists(int idWarehouse);
        public Task<int> CheckIfOrderExists(int idProduct, int amount);
        public Task<bool> CheckIfOrderNotRealised(int idOrder);
        public Task<bool> CheckIfOrderExistsInProduct_Warehouse(int idOrder);
        void UpdateFullfilledAt(int idOrder);
        int InsertIntoProduct_Warehouse(int idOrder, Product product);
    }
}