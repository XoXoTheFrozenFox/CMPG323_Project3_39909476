using Data; // Import necessary data-related namespaces.
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repository
{
    // ProductsRepository class implements IProductsRepository interface.
    public class ProductsRepository : IProductsRepository
    {
        protected readonly SuperStoreContext _context = new SuperStoreContext();

        // GET ALL: Retrieve all products from the database.
        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        // TO DO: Add 'Get By Id' - Retrieve a product by its unique identifier.
        public Product GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        // TO DO: Add 'Create' - Create a new product in the database.
        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // TO DO: Add 'Edit' - Update an existing product in the database.
        public void Edit(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // TO DO: Add 'Delete' - Delete a product by its unique identifier.
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        // TO DO: Add 'Exists' - Check if a product with a specific identifier exists.
        public bool Exists(int id)
        {
            return _context.Products.Any(p => p.ProductId == id);
        }
    }
}