using Models; // Import the necessary models.
using System.Collections.Generic;

namespace EcoPower_Logistics.Repository
{
    // Interface for working with products.
    public interface IProductsRepository
    {
        // Retrieve all products from the repository.
        IEnumerable<Product> GetAll();

        // Retrieve a product by its unique identifier.
        Product GetById(int id);

        // Create a new product in the repository.
        void Create(Product product);

        // Update an existing product in the repository.
        void Edit(Product product);

        // Delete a product by its unique identifier.
        void Delete(int id);

        // Check if a product with a specific identifier exists in the repository.
        bool Exists(int id);
    }
}