using BankApp.Models;
using BankApp.Validations;
using NUnit.Framework;
using Shouldly;
using System;

namespace BankAppTests.ValidatorTests
{
    class LoanDetailsValidatorTest
    {
        private LoanDetailsValidator loanDetailsValidator;
        private LoanDetails loanDetails;
        [SetUp]
        public void Setup()
        {
            loanDetailsValidator = new LoanDetailsValidator();
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
        public void Validate_MailIdNull_ReturnsValidationError()
        {
            loanDetails.MailId = null;
            var result = loanDetailsValidator.Validate(loanDetails);
            result.ShouldContain("MailId Required");
        }

        [Test]
        public void Validate_LoanTypeNull_ReturnsValidationError()
        {
            loanDetails.LoanType = null;
            var result = loanDetailsValidator.Validate(loanDetails);
            result.ShouldContain("LoanType Required");
        }

        [Test]
        public void Validate_AmountInvalid_ReturnsValidationError()
        {
            loanDetails.Amount = -200;
            var result = loanDetailsValidator.Validate(loanDetails);
            result.ShouldContain("Amount must be greater than zero");
        }


        [Test]
        public void Validate_RateOfInterest_Invalid_ReturnsValidationError()
        {
            loanDetails.RateOfInterest = -200;
            var result = loanDetailsValidator.Validate(loanDetails);
            result.ShouldContain("RateOfInteres should be greater than 0");
        }

        [Test]
        public void Validate_DurationOfLoan_Invalid_ReturnsValidationError()
        {
            loanDetails.DurationOfLoan = -200;
            var result = loanDetailsValidator.Validate(loanDetails);
            result.ShouldContain("Duration of loan should be greater than 0 ie; in years");
        }
    }
}
