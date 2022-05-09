using BankApp.Models;
using BankApp.Processor.IProcessor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUpdateAccountProcessor<Customer, ResponseModel> _updateAccountProcessor;
        private readonly ILoanDetailsProcessor<LoanDetails, ResponseModel> _loanProcessor;
        public HomeController(IUpdateAccountProcessor<Customer, ResponseModel> updateAccountProcessor, ILoanDetailsProcessor<LoanDetails, ResponseModel> loanProcessor)
        {
            _updateAccountProcessor = updateAccountProcessor;
            _loanProcessor = loanProcessor;
        }

        [HttpPut("UpdateAccount")]
        public IActionResult UpdateAccount([FromBody] Customer customer)
        {
            var result = _updateAccountProcessor.UpdateAccountDetails(customer);

            switch(result.StateOfModel)
            {
                case ResponseCode.Success: return Ok(result.customer);
                case ResponseCode.ValidationError: return Ok(result.Errors);
                case ResponseCode.Failure: return NotFound("Updation unsucessfull , User doesn't have an active account");
                default :  return BadRequest("Internal Error");
            }            

        }

        [HttpGet("LoanDetails")]
        public IActionResult GetLoanDetails(string mailId)
        {           
            var result = _loanProcessor.GetByMailId(mailId);
            switch (result.StateOfModel)
            {
                case ResponseCode.Success: return Ok(result.LoanDetails);
                case ResponseCode.Failure: return Ok("User has no Loans in active");
                default: return BadRequest("Internal Error");
            }           
        }

        [HttpPost("ApplyLoan")]
        public IActionResult ApplyLoan([FromBody] LoanDetails loanDetails)
        {
            var result = _loanProcessor.ApplyLoan(loanDetails);
            switch (result.StateOfModel)
            {
                case ResponseCode.Success: return Ok("Loan Applied Successfully");
                case ResponseCode.ValidationError: return Ok(result.Errors);
                case ResponseCode.Failure: return NotFound("User does'nt exist in the bank database");
                default: return BadRequest("Internal Error");
            }           
        }
    }
}
