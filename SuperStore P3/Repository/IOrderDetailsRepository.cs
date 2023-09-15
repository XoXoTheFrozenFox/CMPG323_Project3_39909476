using Models;
using System.Collections.Generic;

namespace EcoPower_Logistics.Repository
{
    public interface IOrderDetailsRepository
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);
        void Create(OrderDetail orderDetails);
        void Edit(OrderDetail orderDetails);
        void Delete(int id);
        bool Exists(int id);
    }
}