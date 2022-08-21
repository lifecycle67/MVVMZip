using DemoApp.Model;
using System;
using System.Collections.Generic;

namespace DemoApp.DataAccess
{
    public interface ICustomerRepository
    {
        event EventHandler<CustomerAddedEventArgs> CustomerAdded;

        void AddCustomer(Customer customer);
        bool ContainsCustomer(Customer customer);
        List<Customer> GetCustomers();
    }
}