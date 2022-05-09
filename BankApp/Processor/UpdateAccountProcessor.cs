using BankApp.Models;
using BankApp.Processor.IProcessor;
using BankApp.Repository.IRepository;
using BankApp.Validations;
using Microsoft.Extensions.Logging;
using System;

namespace BankApp.Processor
{
    public class UpdateAccountProcessor : IUpdateAccountProcessor<Customer, ResponseModel>
    {
        private readonly IUpdateAccountRepository _updateAccountRepository;
        private readonly ILogger<UpdateAccountProcessor> _logger;

        public UpdateAccountProcessor(IUpdateAccountRepository updateAccountRepository, ILogger<UpdateAccountProcessor> logger)
        {
            _updateAccountRepository = updateAccountRepository;
            _logger = logger;
        }

        public ResponseModel UpdateAccountDetails(Customer customer)
        {
            ResponseModel responseModel;
            try
            {
                _logger.LogInformation("Update Account Details Process initiated");
                var result = _updateAccountRepository.UpdateAccountDetails(customer);
                CustomerValidator customerValidator = new CustomerValidator();               
                var errors = customerValidator.Validate(customer);
                if (errors.Count > 0)
                {
                    _logger.LogError("Validation errors recorded");
                    responseModel = new ResponseModel()
                    {
                        StateOfModel = ResponseCode.ValidationError,
                        Data = null,
                        Errors = errors
                    };
                    return responseModel;
                }
                if (result != null)
                {
                    _logger.LogInformation("Updation Succesfull");
                    responseModel = new ResponseModel()
                    {
                        StateOfModel = ResponseCode.Success,
                        customer = result,
                        Data = result.ToString(),
                        Errors = null
                    };
                    return responseModel;
                }
                _logger.LogError("Updation Failed");
                responseModel = new ResponseModel()
                {
                    StateOfModel = ResponseCode.Failure,
                    Data = null
                };
                return responseModel;
            }
            catch(Exception )
            {
                _logger.LogWarning("Exception raised");
                responseModel = new ResponseModel()
                {
                    StateOfModel = ResponseCode.InternalError
                };
                return responseModel;
            }  
        }
    }
}
