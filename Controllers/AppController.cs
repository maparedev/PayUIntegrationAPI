using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JckShopping.Data;
using JckShopping.Data.Entities;
using JckShopping.Services;
using JckShopping.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JckShopping.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        
        public IJKCRepository _jkcRepository { get; }

        public AppController(IMailService mailService, IJKCRepository repository)
        {
            _mailService = mailService;
            _jkcRepository = repository;
            
        }
        public IActionResult Index()
        {
            //object obj = Request.Form["status"];
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
                        
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("mapare121@gmail.com", model.Subject, model.Message);
                ViewBag.UserMessage = "Email Sent!!";
                ModelState.Clear();
            }
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }

        
        public IActionResult Shop()
        {
            IEnumerable<Product> products = _jkcRepository.GetAllProducts();
            return View(products);
        }


    }
}
