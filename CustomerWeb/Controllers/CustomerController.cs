using CustomerData.Models;
using CustomerData.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CustomerWeb.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository _customerRepository;

        public CustomerController (ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var customerList = this._customerRepository.FindAll().ToList();
            return View(customerList);
        }

        public IActionResult Details(int id)
        {
            var opera = this._customerRepository.FindByPrimaryKey(id);
            ViewData["Opera"] = opera;
            return View();
        }

        public IActionResult Delete(int id)
        {
            var opera = this._customerRepository.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult New()
        {
            ViewData["Action"] = "New";
            return View("Form", new Customer());
        }

        public IActionResult Edit(int id)
        {
            ViewData["Action"] = "Edit";
            var opera = this._customerRepository.FindByPrimaryKey(id);
            return View("Form", opera);
        }
        public IActionResult Save(string action, Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (action.ToLower().Equals("new"))
                {
                    this._customerRepository.Insert(customer);
                }
                else if (action.ToLower().Equals("edit"))
                {
                    this._customerRepository.Update(customer);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", customer);
            }
        }
    }
}
