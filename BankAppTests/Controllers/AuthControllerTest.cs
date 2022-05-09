using BankApp.Controllers;
using BankApp.Models;
using BankApp.Processor.IProcessor;
using BankApp.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTests.Controllers
{
    class AuthControllerTest
    {
        private AuthController _controller;
        private Mock<ILoginProcessor> _loginProcessor;
        private Mock<IConfiguration> _config;
        private Mock<ILoginRepository> _repo;
        private string token;
        Customer customer;
        ResponseModel responseModel;

        [SetUp]
        public void Setup()
        {
            _loginProcessor = new Mock<ILoginProcessor>();
            _config = new Mock<IConfiguration>();
            _repo = new Mock<ILoginRepository>();
            _controller = new AuthController(_loginProcessor.Object);
            customer = new Customer()
            {
                CustomerName = "John",
                MailId = "abc12@gmail.com",
                AccountType = "Current",
                Password = "asd123546",
                Address = "abcstreet",
                Contact = "8569856985"
            };
            responseModel = new ResponseModel() { Data = token };
        }

        [Test]
        public void LoginSuccess()
        {
            ResponseModel responseModel2 = new ResponseModel() 
            {
                StateOfModel = ResponseCode.SuccessLogin,
                Data = token 
            };
            _repo.Setup(x => x.Login(It.IsAny<Customer>())).Returns(true);
            _loginProcessor.Setup(x => x.Process(It.IsAny<Customer>())).Returns(responseModel);
            Customer customer2 = new Customer()
            {
                CustomerName = "John",
                MailId = "abc12@gmail.com",
                AccountType = "Current",
                Password = "asd123546",
                Address = "abcstreet",
                Contact = "8569856985"
            };
            var result = _controller.Login(customer2) as StatusCodeResult;
            result.StatusCode.ShouldBe(200);            
        }
    }
}
