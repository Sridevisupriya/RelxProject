using BankApp.Models;
using BankApp.Processor;
using BankApp.Repository.IRepository;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace BankAppTests.Processor
{
    public class LoanProcessorTest
    {
        private LoanDetailsProcessor _loanProcessor;
        private Mock<ILoanDetailsRepository> _repo;
        private Mock<ILogger<LoanDetailsProcessor>> _logger;
        private LoanDetails loanDetails;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<LoanDetailsProcessor>>();
            _repo = new Mock<ILoanDetailsRepository>();
            _loanProcessor = new LoanDetailsProcessor(_repo.Object, _logger.Object);
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
        public void ApplyLoanSuccess()
        {
            _repo.Setup(x => x.ApplyLoan(loanDetails)).Returns(loanDetails);
            var result = _loanProcessor.ApplyLoan(loanDetails);
            result.Errors.ShouldBeNull();
        }

        [Test]
        public void ApplyLoanFailure()
        {
            _repo.Setup(x => x.ApplyLoan(loanDetails)).Returns((LoanDetails)null);
            var result = _loanProcessor.ApplyLoan(loanDetails);
            result.Data.ShouldBeNull();
        }

        [Test]
        public void GetLoanDetails_Success()
        {
            string mailId = " su2@hmail.com";
            LoanDetails[] loanDetails = new LoanDetails[]
            {
                new LoanDetails{MailId = "asd@gmail.com",Amount = 250000,LoanType = "CarLoan",DurationOfLoan = 5,RateOfInterest = 2,Date = DateTime.Parse("12/03/2022")},
                new LoanDetails{MailId = "adf@gmail.com",Amount = 250000,LoanType = "CarLoan",DurationOfLoan = 5,RateOfInterest = 2,Date = DateTime.Parse("12/03/2022")},
                new LoanDetails{MailId = "dghf@gmail.com",Amount = 250000,LoanType = "CarLoan",DurationOfLoan = 5,RateOfInterest = 2,Date = DateTime.Parse("12/03/2022")},
            };

            _repo.Setup(x => x.GetByMailId(mailId)).Returns(loanDetails);

            var result = _loanProcessor.GetByMailId(mailId);
            result.Errors.ShouldBeNull();
        }

        [Test]
        public void GetLoanDetails_Failure()
        {
            string mailId = " su2@hmail.com";
            _repo.Setup(x => x.GetByMailId(mailId)).Returns(null as IEnumerable<LoanDetails>);

            var result = _loanProcessor.GetByMailId(mailId);
            result.Data.ShouldBeNull();
        }
    }
}
