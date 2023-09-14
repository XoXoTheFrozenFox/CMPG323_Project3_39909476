using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using EcoPower_Logistics.Repository; // Add the appropriate namespace for your repository

namespace Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;

        // Constructor to inject the IProductsRepository
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            // Retrieve a list of products from the repository and pass it to the view
            var products = _productsRepository.GetAll();
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific product by ID from the repository
            var product = _productsRepository.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            // Display the product details in the view
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            // Display the Create product view
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,ProductName,ProductDescription,UnitsInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                // Create a new product in the repository if the model is valid
                _productsRepository.Create(product);
                return RedirectToAction(nameof(Index));
            }

            // Return the Create view with validation errors if the model is not valid
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific product by ID from the repository for editing
            var product = _productsRepository.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            // Display the Edit product view
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,ProductName,ProductDescription,UnitsInStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the product in the repository if the model is valid
                _productsRepository.Edit(product);
                return RedirectToAction(nameof(Index));
            }

            // Return the Edit view with validation errors if the model is not valid
            return View(product);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific product by ID from the repository for deletion
            var product = _productsRepository.GetById(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            // Display the Delete product view
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_productsRepository.Exists(id))
            {
                return NotFound();
            }

            // Delete the product from the repository
            _productsRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}