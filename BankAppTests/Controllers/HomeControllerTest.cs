using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Controllers;
using BankApp.Models;
using BankApp.Processor.IProcessor;
using BankApp.Repository.IRepository;
using BankApp.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace BankAppTests.Controllers
{
    public class HomeControllerTest
    {
        private HomeController _controller;
        private Mock<ILoanDetailsProcessor<LoanDetails, ResponseModel>> _loanDetailsProcessor;
        private Mock<IUpdateAccountProcessor<Customer, ResponseModel>> _updateAccountProcessor;
        private Mock<IConfiguration> _config;
        private Mock<ILoanDetailsRepository> _loanDetailsRepo;
        private Mock<IUpdateAccountRepository> _updateAccountRepo;
        private Mock<IValidator<Customer>> _validator;
        Customer customer;
        LoanDetails loanDetails;

        [SetUp]
        public void Setup()
        {
            _loanDetailsProcessor = new Mock<ILoanDetailsProcessor<LoanDetails, ResponseModel>>();
            _updateAccountProcessor = new Mock<IUpdateAccountProcessor<Customer, ResponseModel>>();
            _config = new Mock<IConfiguration>();
            _loanDetailsRepo = new Mock<ILoanDetailsRepository>();
            _updateAccountRepo = new Mock<IUpdateAccountRepository>();
            _validator = new Mock<IValidator<Customer>>();
            _controller = new HomeController(_updateAccountProcessor.Object , _loanDetailsProcessor.Object);
            customer = new Customer()
            {
                CustomerName = "John",
                MailId = "abc12@gmail.com",
                AccountType = "Current",
                Password = "asd123546",
                Address = "abcstreet",
                Contact = "8569856985"
            };
            loanDetails = new LoanDetails()
            {
                MailId = "asd@gmail.com",
                Amount = 250000,
                LoanType = "CarLoan",
                DurationOfLoan = 5,
                RateOfInterest = 2,
                Date = DateTime.Parse("12/03/2022")
            };
        }

        [Test]
        public void UpdateAccount_Success()
        {
            ResponseModel responseModel = new ResponseModel()
            {
                StateOfModel = ResponseCode.Success
            };

            _updateAccountRepo.Setup(x => x.UpdateAccountDetails(customer)).Returns(customer);
            _updateAccountProcessor.Setup(x => x.UpdateAccountDetails(It.IsAny<Customer>())).Returns(responseModel);
            Customer customer2 = new Customer()
            {
                CustomerName = "John",
                MailId = "abc12@gmail.com",
                AccountType = "Current",
                Password = "asd123546",
                Address = "abcstreet",
                Contact = "8569856985"
            };
            var result = _controller.UpdateAccount(customer2);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void UpdateAccount_Failure()
        {
            ResponseModel responseModel = new ResponseModel()
            {
                StateOfModel = ResponseCode.Failure
            };

            _updateAccountRepo.Setup(x => x.UpdateAccountDetails(customer)).Returns((Customer)null);
            _updateAccountProcessor.Setup(x => x.UpdateAccountDetails(It.IsAny<Customer>())).Returns(responseModel);
            Customer customer2 = new Customer()
            {
                CustomerName = "John",
                MailId = "abc12@gmail.com",
                AccountType = "Current",
                Password = "asd123546",
                Address = "abcstreet",
                Contact = "8569856985"
            };
            var result = _controller.UpdateAccount(customer2);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public void LoanDetails_Success()
        {
            ResponseModel responseModel = new ResponseModel()
            {
                StateOfModel = ResponseCode.Success
            };

            _loanDetailsRepo.Setup(x => x.ApplyLoan(loanDetails)).Returns(loanDetails);
            _loanDetailsProcessor.Setup(x => x.ApplyLoan(It.IsAny<LoanDetails>())).Returns(responseModel);            
            var result = _controller.ApplyLoan(loanDetails);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetLoanDetailsById_Success()
        {
            ResponseModel responseModel = new ResponseModel()
            {
                StateOfModel = ResponseCode.Success
            };
            string mailId = "sup12@gmail.com";
            LoanDetails[] loanDetails = new LoanDetails[]
            {
                new LoanDetails{MailId = "asd@gmail.com",Amount = 250000,LoanType = "CarLoan",DurationOfLoan = 5,RateOfInterest = 2,Date = DateTime.Parse("12/03/2022")},
                new LoanDetails{MailId = "adf@gmail.com",Amount = 250000,LoanType = "CarLoan",DurationOfLoan = 5,RateOfInterest = 2,Date = DateTime.Parse("12/03/2022")},
                new LoanDetails{MailId = "dghf@gmail.com",Amount = 250000,LoanType = "CarLoan",DurationOfLoan = 5,RateOfInterest = 2,Date = DateTime.Parse("12/03/2022")},
            };
            _loanDetailsRepo.Setup(x => x.GetByMailId(mailId)).Returns(loanDetails);
            _loanDetailsProcessor.Setup(x => x.GetByMailId(It.IsAny<string>())).Returns(responseModel);
            var result = _controller.GetLoanDetails(mailId);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void GetLoanDetailsById_Failure()
        {
            ResponseModel responseModel = new ResponseModel()
            {
                StateOfModel = ResponseCode.Failure
            };
            string mailId = "sup12@gmail.com";
            
            _loanDetailsRepo.Setup(x => x.GetByMailId(mailId)).Returns(null as IEnumerable<LoanDetails>);
            _loanDetailsProcessor.Setup(x => x.GetByMailId(It.IsAny<string>())).Returns(responseModel);
            var result = _controller.GetLoanDetails(mailId);
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public void LoanDetails_Failure()
        {
            ResponseModel responseModel = new ResponseModel()
            {
                StateOfModel = ResponseCode.Failure
            };

            _loanDetailsRepo.Setup(x => x.ApplyLoan(loanDetails)).Returns((LoanDetails)null);
            _loanDetailsProcessor.Setup(x => x.ApplyLoan(It.IsAny<LoanDetails>())).Returns(responseModel);
            var result = _controller.ApplyLoan(loanDetails);
            Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}
