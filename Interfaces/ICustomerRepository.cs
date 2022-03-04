using LibApp.Models;
using System.Collections.Generic;
namespace LibApp.Interfaces
{
  
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(string customerId);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string customerId);

        void Save();
    }
}
