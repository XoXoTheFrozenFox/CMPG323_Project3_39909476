using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using EcoPower_Logistics.Repository; // Add the appropriate namespace for your repository

namespace Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;

        // Constructor to inject the ICustomersRepository
        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        // GET: Customers
        public IActionResult Index()
        {
            // Retrieve a list of customers from the repository and pass it to the view
            var customers = _customersRepository.GetAll();
            return View(customers);
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific customer by ID from the repository
            var customer = _customersRepository.GetById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            // Display the customer details in the view
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            // Display the Create customer view
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerId,CustomerTitle,CustomerName,CustomerSurname,CellPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Create a new customer in the repository if the model is valid
                _customersRepository.Create(customer);
                return RedirectToAction(nameof(Index));
            }

            // Return the Create view with validation errors if the model is not valid
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific customer by ID from the repository for editing
            var customer = _customersRepository.GetById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            // Display the Edit customer view
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerId,CustomerTitle,CustomerName,CustomerSurname,CellPhone")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the customer in the repository if the model is valid
                _customersRepository.Edit(customer);
                return RedirectToAction(nameof(Index));
            }

            // Return the Edit view with validation errors if the model is not valid
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific customer by ID from the repository for deletion
            var customer = _customersRepository.GetById(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            // Display the Delete customer view
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_customersRepository.Exists(id))
            {
                return NotFound();
            }

            // Delete the customer from the repository
            _customersRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}