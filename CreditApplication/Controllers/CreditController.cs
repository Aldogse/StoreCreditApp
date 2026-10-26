using CreditApplication.Database;
using CreditApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO.Customer;
using Models.DTO.Products;
using Models.Users;

namespace CreditApplication.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CreditController : ControllerBase
    {
        private readonly ICreditRepository _creditRepository;

        public CreditController(ICreditRepository creditRepository,ApplicationDbContext applicationDbContext)
        {
            _creditRepository = creditRepository;
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _creditRepository.GetCustomers();

            if(customers.Count == 0 || customers == null)
            {
                return Ok(new
                {
                    message = "No customer found!"
                });
            }

            return Ok(customers);
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody]AddCustomerDTO addCustomerDTO)
        {
            var newCustomer = new Customers
            {
                Name = addCustomerDTO.Name,
            };

             _creditRepository.AddCustomerDetailsAsync(newCustomer);
      
            return Ok(new
            {
                message = "New Customer Added successfully!"
            });
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductDTO product)
        {
            var exist = await _creditRepository.GetProductByName(product.ProductName);

            //check if the product exist
            if(exist != null)
            {
                return BadRequest(new
                {
                    message = $"Product {product.ProductName} already exist!"
                });
            }

            var prod = await _creditRepository.AddProduct(product);

            return Ok(new
            {
                message = $"{prod.ProductName} has been added!"
            });
        }

        [Authorize(Roles = "Owner")]
        [HttpPost("UpdateProduct/{ProductId}")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int ProductId, [FromBody]ProductDTO Product)
        {
           var update = await _creditRepository.UpdateProduct(ProductId, Product);

            if (!update)
            {
                return BadRequest(new
                {
                    message = "Cannot update the Product!"
                });
            }

            return Ok(new
            {
                message = "Product updated successfully!"
            });
        }
    }
}
