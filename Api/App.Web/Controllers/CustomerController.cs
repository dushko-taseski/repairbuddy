using App.BusinessLogicContracts;
using App.Web.Dtos.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("customers")]
        [HttpGet]
        public IActionResult CustomersFromUser()
        {
            var result = _customerService.GetAllForUser(LoggedInUserId);
            return Ok(result.Payload);
        }

        [Route("customer/new")]
        [HttpPost]
        public IActionResult AddNewCustomer([FromBody]NewCustomerDto newCustomerDto)
        {
            var result = _customerService.AddCustomer(
                newCustomerDto.Name, 
                newCustomerDto.Email, 
                newCustomerDto.Address, 
                newCustomerDto.City, 
                newCustomerDto.Phone, 
                LoggedInUserId
            );

            if(!result.IsFaulted)
            {
                return Ok(result.Payload);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [Route("customer/update")]
        [HttpPost]
        public IActionResult UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            var result = _customerService.UpdateCustomer(
                updateCustomerDto.Id, 
                updateCustomerDto.Name, 
                updateCustomerDto.Address, 
                updateCustomerDto.City, 
                updateCustomerDto.Phone
            );

            if (!result.IsFaulted)
            {
                return Ok(result.Payload);
            }

            return NotFound(result.ErrorMessage);
        }

        [Route("customer/delete")]
        [HttpPost]
        public IActionResult DeleteCustomer(int customerId)
        {
            var result = _customerService.DeleteCustomer(customerId);
            if (!result.IsFaulted)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}
