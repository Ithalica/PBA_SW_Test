using HotelBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HotelBooking.Core.Interfaces;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> _repository;

        public CustomersController(IRepository<Customer> repos)
        {
            _repository = repos;
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(_repository.GetAll().Select(CustomerViewModel.FromCustomer).ToList());
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _repository.Get(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(CustomerViewModel.FromCustomer(customer));
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Email")] CustomerInputModel customer)
        {
            if (ModelState.IsValid)
            {
                var cust = new Customer(0, customer.Name)
                {
                    Email = customer.Email
                };

                _repository.TryCreate(cust, out var _);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _repository.Get(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(CustomerInputModel.FromCustomer(customer));
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Email")] CustomerInputModel customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cust = _repository.Get(customer.Id);
                    cust.Email = customer.Email;
                    //TODO: Consider if the customer name should be updatable.
                    //cust.Name = customer.Name;

                    _repository.TryUpdate(cust, out var _);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.Get(customer.Id) == null)
                    {
                        return NotFound();
                    }
                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _repository.Get(id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            return View(CustomerViewModel.FromCustomer(customer));
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
