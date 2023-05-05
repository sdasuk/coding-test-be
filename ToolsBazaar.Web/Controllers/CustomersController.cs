using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;

namespace ToolsBazaar.Web.Controllers;

public record CustomerDto(string Name);

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger, ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }


    [HttpGet]
    public IEnumerable<Customer> GetAll() 
    { 
        return _customerRepository.GetAll();
    }

    [Authorize]
    [HttpPut("{customerId}/{name}")]
    public void UpdateCustomerName(int customerId, [FromRoute] CustomerDto dto)
    {
        _logger.LogInformation($"Updating customer #{customerId} name...");
        try
        {
            _customerRepository.UpdateCustomerName(customerId, dto.Name);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        
    }

    [HttpGet]
    [Route("top5")]
    public IEnumerable<Customer> GetTop5([FromQuery]DateTime startDate, [FromQuery]DateTime endDate)
    {
        var results = _orderRepository.GetAll()
                .Where(o=> o.Date >= startDate && o.Date <= endDate)
                .GroupBy(o => o.Customer)
                .OrderByDescending(og => og.Count())
                .Take(5)
                .Select(og => new
                {
                    Customer = _customerRepository.GetAll().Single(c => c.Equals(og.Key)),
                    Orders = og
                })

                .Select(c => new Top5Customer
                {
                    Id = c.Customer.Id,
                    Name = c.Customer.Name,
                    Email = c.Customer.Email,
                    OrderCount = c.Orders.Count()
                });

        return results;
    }

    [HttpHead]
    [Route("healthcheck")]
    public OkResult HealthCheck()
    {
       return Ok();
    }
}