using JckShopping.Data.Entities;
using System.Collections.Generic;

namespace JckShopping.Data
{
    public interface IJKCRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);        
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItems);
        Order GetAllOrderById(string userName, int id);
        void AddEntity(Order model);
        public bool SaveAll();

    }
}