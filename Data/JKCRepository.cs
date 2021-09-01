using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JckShopping.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JckShopping.Data
{
    public class JKCRepository : IJKCRepository
    {
        private readonly JKCContext _context;

        private ILogger<JKCRepository> _logger;
        public JKCRepository(JKCContext context, ILogger<JKCRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntity(Order model)
        {
            _context.Add(model);
        }

        public Order GetAllOrderById(string userName,int id)
        {
            return _context.Orders                        
                       .Include(o => o.Items)
                       .ThenInclude(p => p.Product)
                       .Where(o => o.Id == id && o.User.UserName == userName)
                       .FirstOrDefault();
        }

        public IEnumerable<Order> GetAllOrders(bool includesItems)
        {
            if (includesItems)
            {

                return _context.Orders
                            .Include(o => o.Items)
                            .ThenInclude(p => p.Product)
                            .ToList();
            }
            else
            {
                return _context.Orders.ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            if (includeItems)
            {
                return _context.Orders
                            .Where(o => o.User.UserName == username)
                            .Include(o => o.Items)
                            .ThenInclude(p => p.Product)
                            .ToList();
            }
            else
            {
                return _context.Orders
                            .Where(o => o.User.UserName == username).ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");
                return _context.Products.OrderByDescending(p => p.Category).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Products {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
