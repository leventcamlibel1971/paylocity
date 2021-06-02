using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Paylocity.Payroll.ApiService.Filters;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;

namespace Paylocity.Payroll.ApiService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPaycheckFacade _paycheckFacade;

        public PayrollController(ILogger<PayrollController> logger,
            IPaycheckFacade paycheckFacade)
        {
            _paycheckFacade = paycheckFacade;
        }

        [ServiceFilter(typeof(ModelStateValidate))]
        [HttpPost("paycheck/v1")]
        public IActionResult GeneratePaycheck([FromBody] EmployeeModel employeeModel)
        {
            var payCheck = _paycheckFacade.Calculate(employeeModel);
            return Ok(payCheck);
        }
    }
}