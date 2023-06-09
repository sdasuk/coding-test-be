﻿using ToolsBazaar.Domain.CustomerAggregate;

namespace ToolsBazaar.Persistence;

public class CustomerRepository : ICustomerRepository
{
    public IEnumerable<Customer> GetAll() => DataSet.AllCustomers;

    public void UpdateCustomerName(int customerId, string name)
    {
        var customer = DataSet.AllCustomers.FirstOrDefault(c => c.Id == customerId);

        if (customer == null)
        {
            throw new Exception("Id not found");
        }
        else
        {
            customer.UpdateName(name); 
        }
    }
}