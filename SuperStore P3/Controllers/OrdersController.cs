using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models; // Consider using a more specific namespace to avoid naming conflicts.
using EcoPower_Logistics.Repository;

namespace Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomersRepository _customersRepository;

        public OrdersController(IOrdersRepository ordersRepository, ICustomersRepository customersRepository)
        {
            _ordersRepository = ordersRepository;
            _customersRepository = customersRepository;
        }

        // GET: Orders
        public IActionResult Index()
        {
            // Get all orders and pass them to the view.
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

            // Get the order by ID.
            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            // Load customer details associated with the order.
            order.Customer = _customersRepository.GetById(order.CustomerId);

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            // Provide a list of customers for selection in the view.
            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerId");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            // Validate and create a new order.
            if (ModelState.IsValid)
            {
                _ordersRepository.Create(order);
                return RedirectToAction(nameof(Index));
            }

            // If there are validation errors, redisplay the form with the submitted data.
            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the order by ID for editing.
            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            // Provide a list of customers for selection in the view.
            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerId", order.CustomerId);
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

            // Validate and update the order.
            if (ModelState.IsValid)
            {
                _ordersRepository.Edit(order);
                return RedirectToAction(nameof(Index));
            }

            // If there are validation errors, redisplay the form with the submitted data.
            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the order by ID for deletion.
            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            // Load customer details associated with the order.
            order.Customer = _customersRepository.GetById(order.CustomerId);

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Check if the order exists and delete it.
            if (!_ordersRepository.Exists(id))
            {
                return NotFound();
            }
            _ordersRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
