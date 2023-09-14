using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using EcoPower_Logistics.Repository; // Add the appropriate namespace for your repository

namespace Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;

        // Constructor to inject the IOrdersRepository
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        // GET: Orders
        public IActionResult Index()
        {
            // Retrieve a list of orders from the repository and pass it to the view
            var orders = _ordersRepository.GetAll();
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific order by ID from the repository
            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            // Display the order details in the view
            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            // Display the Create order view
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                // Create a new order in the repository if the model is valid
                _ordersRepository.Create(order);
                return RedirectToAction(nameof(Index));
            }

            // Return the Create view with validation errors if the model is not valid
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific order by ID from the repository for editing
            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            // Display the Edit order view
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the order in the repository if the model is valid
                _ordersRepository.Edit(order);
                return RedirectToAction(nameof(Index));
            }

            // Return the Edit view with validation errors if the model is not valid
            return View(order);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve a specific order by ID from the repository for deletion
            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            // Display the Delete order view
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_ordersRepository.Exists(id))
            {
                return NotFound();
            }

            // Delete the order from the repository
            _ordersRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
