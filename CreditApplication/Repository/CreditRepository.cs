using System.Threading.Tasks;
using CreditApplication.Database;
using CreditApplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO.Products;
using Models.Users;

namespace CreditApplication.Repository
{
    public class CreditRepository : ICreditRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CreditRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //CUSTOMERS
        public async void AddCreditDetails(Credit credit)
        {
            await _dbContext.Credits.AddAsync(credit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customers> AddCustomerCredit(Customers customers)
        {
            await _dbContext.Customers.AddAsync(customers);
            await _dbContext.SaveChangesAsync();
            return customers;
        }

        //THIS IS FOR ADDING NEW CUSTOMER DETAILS ON THE DATABASE
        public async  void AddCustomerDetailsAsync(Customers customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async void DeleteCustomerById(int Id)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(i => i.CustomerId == Id);
             _dbContext.Remove(customer);
            await _dbContext.SaveChangesAsync();
           
        }

        public async Task<List<Customers>> GetCustomers()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return customers;
        }

        public Task<Customers> UpdateCreditDetails(int Id)
        {
            throw new NotImplementedException();
        }

        //PRODUCTS
        public async Task<Products> AddProduct(ProductDTO product)
        {
            var newProduct = new Products
            {
                ProductName = product.ProductName.ToLower(),
                Price = product.Price,
            };

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();

            return newProduct;
        }

        public async Task<Products> GetProductByName(string ProductName)
        {
            var exist = await _dbContext.Products
                .FirstOrDefaultAsync(i => i.ProductName.ToLower() == ProductName.ToLower());

            return exist;
        }

        public async Task<Products> GetProductById(int ProductId)
        {
            return  await _dbContext.Products.FindAsync(ProductId);
        }

        public async Task<bool> UpdateProduct(int ProductId, ProductDTO updatedProduct)
        {
           var update =  await _dbContext.Products.Where(i => i.ProductId == ProductId)
                .ExecuteUpdateAsync
                (i => i.SetProperty(i => i.ProductName, updatedProduct.ProductName)
                .SetProperty(i => i.Price, updatedProduct.Price));

            return update > 0;
        }
    }
}
