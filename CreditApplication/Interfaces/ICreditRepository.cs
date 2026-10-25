using Models;
using Models.DTO.Products;
using Models.Users;

namespace CreditApplication.Interfaces
{
    public interface ICreditRepository
    {
        //CUSTOMER QUERIES
        Task<Customers> AddCustomerCredit(Customers customers);
        void AddCreditDetails(Credit credit);
        void DeleteCustomerById(int Id);
        Task<Customers> UpdateCreditDetails(int Id);
        Task<List<Customers>> GetCustomers();
        void AddCustomerDetailsAsync(Customers customer);

        //PRODUCTS QUERIES
        Task<Products> GetProduct(string ProductName);
        Task<Products> AddProduct(ProductDTO product);
    }
}
