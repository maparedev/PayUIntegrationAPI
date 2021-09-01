using JckShopping.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace JckShopping.Data
{
    public class JCKSeeder
    {
        private readonly JKCContext _context;
        private IWebHostEnvironment _envrinoment;
        private readonly UserManager<JKCStoreUser> _userManager;


        public JCKSeeder(JKCContext context, IWebHostEnvironment envrinoment, UserManager<JKCStoreUser> userManager)
        {
            _context = context;
            _envrinoment = envrinoment;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            JKCStoreUser user = await _userManager.FindByEmailAsync("mapare121@gmail.com");
            if (user == null)
            {
                user = new JKCStoreUser()
                {
                    FirstName = "Vivek",
                    LastName = "Mapare",
                    Email = "mapare121@gmail.com",
                    UserName = "mapare121@gmail.com",
                    PhoneNumber = "959515951"
                };
                var result = await _userManager.CreateAsync(user, "P@ssword1");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }



            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_envrinoment.ContentRootPath, "Data\\art.json");
                var jsonFile = File.ReadAllText(filePath);
                List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonFile);

                _context.Products.AddRange(products);


                var order = _context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order == null)
                {   
                    order = new Order()
                    {
                        User = user,
                        OrderDate = DateTime.Today,
                        OrderNumber = "10000",
                        Items = new List<OrderItem>()
                        {
                            new OrderItem()
                            {
                                Product = products.First(),
                                Quantity = 5,
                                UnitPrice = products.First().Price
                            }
                        }
                    };
                    _context.Orders.Add(order);
                }

                PaymentGateways pm = _context.PaymentGateways.FirstOrDefault();
                if(pm == null)
                {
                    pm = new PaymentGateways
                    {
                        Name = "PayU Money",
                        MERCHANT_KEY = "BWT6ez",
                        HashSequence = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10",
                        SALT = "Up7i5j4L",
                        PAYU_BASE_URL = "https://test.payu.in/_payment",
                        SUrl = "http://localhost:8080",
                        FUrl = "http://localhost:8080",
                        CUrl = "http://localhost:8080",
                        User = user,
                        Active = true
                    };
                }

                _context.PaymentGateways.Add(pm);


                _context.SaveChanges();
            }
        }
    }
}
