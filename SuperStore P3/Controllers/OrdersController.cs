using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
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

        public IActionResult Index()
        {
            var orders = _ordersRepository.GetAll();
            return View(orders);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                _ordersRepository.Create(order);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerName", order.CustomerId);
            return View(order);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerName", order.CustomerId);
            return View(order);
        }

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
                _ordersRepository.Edit(order);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CustomerId = new SelectList(_customersRepository.GetAll(), "CustomerId", "CustomerName", order.CustomerId);
            return View(order);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _ordersRepository.GetById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!_ordersRepository.Exists(id))
            {
                return NotFound();
            }

            _ordersRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
