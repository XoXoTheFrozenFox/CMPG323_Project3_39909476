using Models; // Import the necessary models.
using System.Collections.Generic;

namespace EcoPower_Logistics.Repository
{
    // Interface for working with customers.
    public interface ICustomersRepository
    {
        // Retrieve all customers from the repository.
        IEnumerable<Customer> GetAll();

        // Retrieve a customer by their unique identifier.
        Customer GetById(int id);

        // Create a new customer in the repository.
        void Create(Customer customer);

        // Update an existing customer in the repository.
        void Edit(Customer customer);

        // Delete a customer by their unique identifier.
        void Delete(int id);

        // Check if a customer with a specific identifier exists in the repository.
        bool Exists(int id);
    }
}