using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        protected readonly SuperStoreContext _context = new SuperStoreContext();

        // GET ALL: Customers
        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
        // TO DO: Add ‘Get By Id’
        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }
        // TO DO: Add ‘Create’
        public void Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        // TO DO: Add ‘Edit’
        public void Edit(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
        }
        // TO DO: Add ‘Delete’
        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
        // TO DO: Add ‘Exists’
        public bool Exists(int id)
        {
            return _context.Orders.Any(o => o.OrderId == id);
        }


    }
}
