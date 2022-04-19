using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Customer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeneralStoreMVC.Controllers
{
    public class CustomerController : Controller
    { 

        private readonly GeneralStoreDbContext _ctx;
        public CustomerController(GeneralStoreDbContext ctx)
    {
        _ctx = ctx;
    }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var customers = _ctx.Customers.Select(customer => new CustomerIndexModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            });
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMsg"] = "Model State is Invalid";
                return View(model);
            }
            _ctx.Customers.Add(new Customer
            {
                Name = model.Name,
                Email = model.Email
            });
            if (_ctx.SaveChanges()==1)
            {
                return Redirect("/Customer");
            }
            TempData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }

        // Get: customer/details/{id}
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDetailModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerEditModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, CustomerEditModel model)
        {
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Name = model.Name;
            customer.Email = model.Email;

            if (_ctx.SaveChanges() == 1)
            {
                return Redirect("/customer");
            }

            ViewData["ErrorMsg"] = "Unable to save to the database. Please try again later.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            var model = new CustomerDetailModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(int? id, CustomerDetailModel model)
        {
            var customer = _ctx.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();
            return Redirect("/Customer");
        }
    }
}
