using BankApp.Models;
using BankApp.Validations;
using NUnit.Framework;
using Shouldly;

namespace BankAppTests.ValidatorTests
{
    class CustomerValidatorTest
    {
        private CustomerValidator customerValidator;
        private Customer customer;
        [SetUp]
        public void Setup()
        {
            customerValidator = new CustomerValidator();
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
        public void Validate_MailIdNull_ReturnsValidationError()
        {
            customer.MailId = null;
            var result = customerValidator.Validate(customer);
            result.ShouldContain("MailId is Required to login");
        }


        [Test]
        public void Validate_PasswordNull_ReturnsValidationError()
        {
            customer.Password = null;
            var result = customerValidator.Validate(customer);
            result.ShouldContain("Password is required to login");
        }

        [Test]
        public void Validate_MailIdInValidFormat_ReturnsValidationError()
        {
            customer.MailId = "ase#4";
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("MailId should be XXXX@gmail.com");
        }

        [Test]
        public void Validate_ContactInValidFormat_ReturnsValidationError()
        {
            customer.Contact = "gfte";
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("Contact must be 10 digits");
        }

        [Test]
        public void Validate_ContactEmpty_ReturnsValidationError()
        {
            customer.Contact = string.Empty;
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("Contact details are required");
        }

        [Test]
        public void Validate_AddressEmpty_ReturnsValidationError()
        {
            customer.Address = string.Empty;
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("Address is Required");
        }

        [Test]
        public void Validate_PasswordInvalid_ReturnsValidationError()
        {
            customer.Password = "ade3";
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("Expecting Password of Length 8 characters");
        }

        [Test]
        public void Validate_NameEmpty_ReturnsValidationError()
        {
            customer.CustomerName = string.Empty;
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("CustomerName is Required");
        }

        [Test]
        public void Validate_AccountTypeEmpty_ReturnsValidationError()
        {
            customer.AccountType = string.Empty;
            var result = customerValidator.ValidateAllFields(customer);
            result.ShouldContain("Account Type is Required");
        }
    }
}
