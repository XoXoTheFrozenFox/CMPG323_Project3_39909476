using Data; // Import necessary data-related namespaces.
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repository
{
    // CustomersRepository class implements ICustomersRepository interface.
    public class CustomersRepository : ICustomersRepository
    {
        protected readonly SuperStoreContext _context = new SuperStoreContext();

        // GET ALL: Retrieve all customers from the database.
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        // TO DO: Add 'Get By Id' - Retrieve a customer by their unique identifier.
        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.CustomerId == id);
        }

        // TO DO: Add 'Create' - Create a new customer in the database.
        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        // TO DO: Add 'Edit' - Update an existing customer in the database.
        public void Edit(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // TO DO: Add 'Delete' - Delete a customer by their unique identifier.
        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        // TO DO: Add 'Exists' - Check if a customer with a specific identifier exists.
        public bool Exists(int id)
        {
            return _context.Customers.Any(c => c.CustomerId == id);
        }
    }
}