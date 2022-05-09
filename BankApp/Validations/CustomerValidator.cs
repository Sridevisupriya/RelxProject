using BankApp.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankApp.Validations
{
    public class CustomerValidator : IValidator<Customer>
    {
        LinkedList<string> validationErrors = new LinkedList<string>();
        public LinkedList<string> Validate(Customer customer)
        {
            if (customer == null)
            {
                validationErrors.AddLast("Request cannot be null");
                return validationErrors;
            }
            if (customer.Password == null)
            {
                validationErrors.AddLast("Password is required to login");
            }
            if (customer.MailId == null)
            {
                validationErrors.AddLast("MailId is Required to login");
            }

            return validationErrors;
        }

        public LinkedList<string> ValidateAllFields(Customer customer)
        {
            if (customer.CustomerName == string.Empty)
            {
                validationErrors.AddLast("CustomerName is Required");
            }

            if (customer.Password.Length < 8)
            {
                validationErrors.AddLast("Expecting Password of Length 8 characters");
            }

            if (customer.Address == string.Empty)
            {
                validationErrors.AddLast("Address is Required");
            }

            if (customer.AccountType == string.Empty)
            {
                validationErrors.AddLast("Account Type is Required");
            }

            if (customer.MailId == null)
            {
                validationErrors.AddLast("MailId Required");
            }

            if (customer.MailId != null)
            {
                var isValid = IsValidFormat(Constants.MAILID, customer.MailId);
                if (!isValid)
                {
                    validationErrors.AddLast("MailId should be XXXX@gmail.com");
                }
            }

            if (customer.Contact == string.Empty)
            {
                validationErrors.AddLast("Contact details are required");
            }

            if (customer.Contact != null)
            {
                var isValid = IsValidFormat(Constants.ContactNumericFormat, customer.Contact);
                if (!isValid)
                {
                    validationErrors.AddLast("Contact must be 10 digits");
                }
            }

            return validationErrors;
        }

        public bool IsValidFormat(string regex, string value)
        {
            Regex regex1 = new Regex(regex);
            var result = regex1.IsMatch(value);
            return result;
        }
    }
}
