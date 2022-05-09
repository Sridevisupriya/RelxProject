using BankApp.Models;
using BankApp.Processor;
using BankApp.Repository.IRepository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace BankAppTests.Processor
{
    public class UpdateProcessorTest
    {
        private UpdateAccountProcessor _updateAccountProcessor;
        private Mock<IUpdateAccountRepository> _repo;
        private Mock<ILogger<UpdateAccountProcessor>> _logger;
        private Customer customer;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<UpdateAccountProcessor>>();
            _repo = new Mock<IUpdateAccountRepository>();
            _updateAccountProcessor = new UpdateAccountProcessor(_repo.Object, _logger.Object);
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
        public void UpdateAccount_Success()
        {
            _repo.Setup(x => x.UpdateAccountDetails(customer)).Returns(customer);
            var result = _updateAccountProcessor.UpdateAccountDetails(customer);
            result.Errors.ShouldBeNull();
        }

        [Test]
        public void UpdateAccount_Failure()
        {
            _repo.Setup(x => x.UpdateAccountDetails(customer)).Returns((Customer)null);
            var result = _updateAccountProcessor.UpdateAccountDetails(customer);
            result.Data.ShouldBeNull();
        }
    }
}
