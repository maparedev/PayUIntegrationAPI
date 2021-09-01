using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JckShopping.Data;
using JckShopping.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JckShopping.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IJKCRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IJKCRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Faild to get data: {ex}");
                return BadRequest($"Faild to get data");
            }

        }

    }
}
