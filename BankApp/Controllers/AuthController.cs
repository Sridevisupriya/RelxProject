using BankApp.Models;
using BankApp.Processor.IProcessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankApp.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly ILoginProcessor _loginProcessor;

        public AuthController(ILoginProcessor loginProcessor)
        {
            _loginProcessor = loginProcessor;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Customer customer)
        {
            var result = _loginProcessor.Process(customer);

            switch (result.StateOfModel)
            {
                case ResponseCode.SuccessLogin:
                    return Ok(new { token = result.Data });
                case ResponseCode.SuccessRegistration:
                    return Ok("Registered Customer Id is " + result.Data);
                case ResponseCode.ValidationError:
                    return Ok(result.Errors);
                case ResponseCode.Failure:
                    return BadRequest("Login unsuccessfull , Please check the details");               
                case ResponseCode.InternalError:
                    return BadRequest("Exception Occured" + result.Data);
                default:
                    return BadRequest("Internal error");
            }
        }
    }
}

