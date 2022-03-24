using Microsoft.AspNetCore.Mvc;
using Services;

namespace Ensek.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="customerService"></param>
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        /// <summary>
        /// Post endpoint used to upload all customers/accounts present in the uploaded csv file
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = _customerService.GetAll();

            return Ok(customers);
        }

        /// <summary>
        /// Post endpoint used to upload all customers/accounts present in the uploaded csv file
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile csv)
        {
            var result = await _customerService.SaveOrUpdate(csv);
            
            return Ok(result);
        }
    }
}