using BankApp.Models;
using BankApp.Processor.IProcessor;
using BankApp.Repository.IRepository;
using BankApp.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApp.Processor
{
    public class LoginProcessor : ILoginProcessor
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginProcessor> _logger;
        private IConfiguration _config;

        public LoginProcessor(ILoginRepository loginRepository, ILogger<LoginProcessor> logger, IConfiguration config)
        {
            _loginRepository = loginRepository;
            _logger = logger;
            _config = config;
        }

        public ResponseModel Process(Customer customer)
        {
            ResponseModel responseModel;
            try
            {
                responseModel = Login(customer);
            }
            catch(Exception e)
            {                
                responseModel = new ResponseModel()
                {
                    StateOfModel = ResponseCode.InternalError,
                    Data = e.ToString()
                };                
            }
            return responseModel;
        }

        public ResponseModel Login(Customer customer)
        {
            _logger.LogInformation("Login Process Initiated");
            CustomerValidator customerValidator = new CustomerValidator();
            var errors = customerValidator.Validate(customer);
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

            var result = _loginRepository.Login(customer);
            if (result)
            {
                _logger.LogInformation("Login Successfull");
                var token = GenerateJSONWebToken(customer);
                _logger.LogInformation("JSON Web Token generated Successfully");
                ResponseModel responseModelSuccess = new ResponseModel()
                {
                    StateOfModel = ResponseCode.SuccessLogin,
                    Data = token,
                    Errors = null

                };
                return responseModelSuccess;
            }

            _logger.LogInformation("User doesn't exist in database, so Registration Process initiated");
            var registerResponse = Register(customer);
            return registerResponse;
        }

        public string GenerateJSONWebToken(Customer userInfo)
        {
            _logger.LogInformation("JSON Web Token generation Started");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDEscriptor = new SecurityTokenDescriptor
            {                
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(tokenKey),
                                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDEscriptor);
            return tokenHandler.WriteToken(token);
        }

        public ResponseModel Register(Customer customer)
        {
            CustomerValidator customerValidator = new CustomerValidator();
            var errors = customerValidator.ValidateAllFields(customer);
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

            var result = _loginRepository.Register(customer);
            if (result != null)
            {
                _logger.LogInformation("Registration Successfull");
                ResponseModel responseModel = new ResponseModel()
                {
                    StateOfModel = ResponseCode.SuccessRegistration,
                    Data = result.CustomerId.ToString(),
                    Errors = null
                };

                return responseModel;
            }
            else
            {
                _logger.LogError("Registration unsuccessfull");
                ResponseModel responseModelFailure = new ResponseModel()
                {
                    StateOfModel = ResponseCode.Failure,
                    Data = null
                };
                return responseModelFailure;
            }
        }
    }
}
