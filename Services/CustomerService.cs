using CustomerManager.Data;
using CustomerManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Services
{
    public class CustomerService
    {
            private readonly CustomerRepository _repository;

            public CustomerService(CustomerRepository repository)
            {
                _repository = repository;
            }

            public List<Customer> GetCustomers()
            {
                return _repository.GetAll();
            }

            public void AddCustomer(Customer customer)
            {
                
                _repository.Add(customer);
            }

            public void UpdateCustomer(Customer customer)
            {
                _repository.Update(customer);
            }

            public void DeleteCustomer(int id)
            {
                _repository.Delete(id);
            }
        }
    }
