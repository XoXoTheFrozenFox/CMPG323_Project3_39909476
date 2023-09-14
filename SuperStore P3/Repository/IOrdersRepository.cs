using Models; // Import the necessary models.
using System.Collections.Generic;

namespace EcoPower_Logistics.Repository
{
    public interface IOrdersRepository
    {
        // Define an interface for working with orders.

        // Retrieve all orders.
        IEnumerable<Order> GetAll();

        // Retrieve an order by its unique identifier.
        Order GetById(int id);

        // Create a new order.
        void Create(Order order);

        // Edit an existing order.
        void Edit(Order order);

        // Delete an order by its unique identifier.
        void Delete(int id);

        // Check if an order with a specific identifier exists.
        bool Exists(int id);

    }
}