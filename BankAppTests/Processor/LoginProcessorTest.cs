using BankApp.Models;
using BankApp.Processor;
using BankApp.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankAppTests.Processor
{
    public class LoginProcessorTest
    {
        private LoginProcessor _loginProcessor;
        private Mock<ILoginRepository> _repo;
        private Mock<ILogger<LoginProcessor>> _logger;
        private  Mock<IConfiguration> _config;
        private Customer customer;


        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<LoginProcessor>>();
            _repo = new Mock<ILoginRepository>();
            _config = new Mock<IConfiguration>();
            _config.Setup(c => c["Jwt:Key"]).Returns("ThisismySecretKey");

            _loginProcessor = new LoginProcessor(_repo.Object, _logger.Object, _config.Object);
            customer = new Customer()
            {
                CustomerName = "John",
                MailId = "abc12@gmail.com",
                AccountType = "Current",
                Password = "asd123546",
                Address = "abcstreet",
                Contact = "8569856985"
            };           
        }   
        
        [Test]
        public void LoginSuccess()
        {
            _repo.Setup(x => x.Login(customer)).Returns(true);
            var result = _loginProcessor.Login(customer);
            result.Data.ShouldNotBeNull();
        }

        [Test]
        public void RegistrationSuccess()
        {
            _repo.Setup(x => x.Register(customer)).Returns(customer);
            var result = _loginProcessor.Login(customer);
            result.Errors.ShouldBeNull();
            result.Data.ShouldNotBeNull();
        }

        [Test]
        public void RegistrationFailure()
        {
            _repo.Setup(x => x.Register(customer)).Returns((Customer)null);
            var result = _loginProcessor.Login(customer);
            result.Data.ShouldBeNull();
        }
    }
}
