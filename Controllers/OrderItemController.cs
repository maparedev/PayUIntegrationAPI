using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JckShopping.Data;
using JckShopping.Data.Entities;
using JckShopping.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JckShopping.Controllers
{

    [Route("api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemController : Controller
    {
        private readonly IJKCRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderItemController> _logger;
        public OrderItemController(IJKCRepository jkc,IMapper mapper, ILogger<OrderItemController> logger)
        {
            _repository = jkc;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetAllOrderById(User.Identity.Name, orderId);

            if(order!= null)
            {
                var mpObject = _mapper.Map<IEnumerable<OrderItemViewModel>>(order.Items);
                return Ok(mpObject);
            }

            return NotFound();

        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetAllOrderById(User.Identity.Name, orderId);

            if (order != null)
            {
                var mpObject = order.Items.Where(i => i.Id == id).FirstOrDefault();
                return Ok(_mapper.Map<OrderItem,OrderItemViewModel>(mpObject));
            }
            return NotFound();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
