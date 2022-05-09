using BankApp.Models;
using BankApp.Processor.IProcessor;
using BankApp.Repository.IRepository;
using BankApp.Validations;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Processor
{
    public class LoanDetailsProcessor : ILoanDetailsProcessor<LoanDetails, ResponseModel>
    {
        private readonly ILoanDetailsRepository _loanDetailsRepository;
        private readonly ILogger<LoanDetailsProcessor> _logger;

        public LoanDetailsProcessor(ILoanDetailsRepository loanDetailsRepository, ILogger<LoanDetailsProcessor> logger)
        {
            _loanDetailsRepository = loanDetailsRepository;
            _logger = logger;
        }
        
        public ResponseModel ApplyLoan(LoanDetails loanData)
        {
            _logger.LogInformation("Apply Loan Process Initiated");
            LoanDetailsValidator loanDetailsValidator = new LoanDetailsValidator();
            var errors = loanDetailsValidator.Validate(loanData);
            if (errors.Count > 0)
            {
                _logger.LogError("Validation errors recorded");
                ResponseModel responseModel = new ResponseModel()
                {
                    StateOfModel = ResponseCode.ValidationError,
                    Data = null,
                    Errors = errors
                };
                return responseModel;
            }
            var result = _loanDetailsRepository.ApplyLoan(loanData);
            if (result != null)
            {
                _logger.LogInformation("Loan Application Successfull");
                ResponseModel responseModelSuccess = new ResponseModel()
                {
                    StateOfModel = ResponseCode.Success,
                    LoanDetails = result as IEnumerable<LoanDetails>
                };
                return responseModelSuccess;
            }
            else
            {
                _logger.LogInformation("Loan Application Failed");
                ResponseModel responseModelFailure = new ResponseModel()
                {
                    StateOfModel = ResponseCode.Failure,
                    Data = null
                };
                return responseModelFailure;
            }
        }

        public ResponseModel GetByMailId(string mailId)
        {
            _logger.LogInformation("Search Process Initiated");
            var result = _loanDetailsRepository.GetByMailId(mailId); 
            if(result==null)
            {
                _logger.LogInformation("Search Failed");
                ResponseModel responseModelFailure = new ResponseModel()
                {
                    StateOfModel = ResponseCode.Failure,
                    Data = null
                };
                return responseModelFailure;
            }
            else
            {
                _logger.LogInformation("Database search Successfull");
                ResponseModel responseModelSuccess = new ResponseModel()
                {
                    StateOfModel = ResponseCode.Success,
                    Errors = null,
                    LoanDetails = result
                };
                return responseModelSuccess;
            }
            
        }
    }
}
