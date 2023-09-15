using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        protected readonly SuperStoreContext _context = new SuperStoreContext();

        public IEnumerable<OrderDetail> GetAll() => _context.OrderDetails;
        public OrderDetail GetById(int id) => _context.OrderDetails.Find(id);

        public void Create(OrderDetail orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            _context.SaveChanges();
        }

        public void Edit(OrderDetail orderDetails)
        {
            _context.Entry(orderDetails).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var orderDetails = GetById(id);
            if (orderDetails != null)
            {
                _context.OrderDetails.Remove(orderDetails);
                _context.SaveChanges();
            }
        }

        public bool Exists(int id) => _context.OrderDetails.Any(od => od.OrderDetailsId == id);
    }
}