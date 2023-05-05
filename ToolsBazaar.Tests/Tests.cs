using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Persistence;
using ToolsBazaar.Web.Controllers;

namespace ToolsBazaar.Tests;

public class Tests
{
    private readonly Mock<ICustomerRepository> _mockCustomer;
    private readonly Mock<IOrderRepository> _mockOrder;
    private readonly Mock<ILogger<CustomersController>> _mockLogger;
    private readonly CustomersController _controller;

    public Tests()
    {
        _mockLogger = new Mock<ILogger<CustomersController>>();
        _mockCustomer = new Mock<ICustomerRepository>();
        _mockOrder = new Mock<IOrderRepository>();
        _controller = new CustomersController(_mockLogger.Object, _mockCustomer.Object, _mockOrder.Object);
    }

    [Fact]
    public void SampleTest()
    {
        var x = 10;

        x.Should().Be(10);
    }

    [Fact]
    public void Top5CustomersTest()
    {
        var startDate = new DateTime(2015, 1, 1);
        var endDate = new DateTime(2022, 12, 31);

        var orders = DataSet.AllOrders;
        _mockOrder.Setup(m => m.GetAll()).Returns(orders);

        var customers = DataSet.AllCustomers;
        _mockCustomer.Setup(c => c.GetAll()).Returns(customers);

        _mockOrder.Verify();
        _mockCustomer.Verify();

        var result = _controller.GetTop5(startDate, endDate);

        Assert.Equal(5, result.Count());
    }
}