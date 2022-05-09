using BankApp.Models;
using System.Collections.Generic;

namespace BankApp.Validations
{
    public class LoanDetailsValidator : IValidator<LoanDetails>
    {
        LinkedList<string> validationErrors = new LinkedList<string>();
        public LinkedList<string> Validate(LoanDetails request)
        {
            if (request == null)
            {
                validationErrors.AddLast("Request must have value");
                return validationErrors;
            }
            if (request.MailId == string.Empty || request.MailId == null)
            {
                validationErrors.AddLast("MailId Required");
            }
            if (request.LoanType == string.Empty || request.LoanType == null)
            {
                validationErrors.AddLast("LoanType Required");
            }
            if (request.RateOfInterest <= 0)
            {
                validationErrors.AddLast("RateOfInteres should be greater than 0");
            }
            if (request.DurationOfLoan <= 0)
            {
                validationErrors.AddLast("Duration of loan should be greater than 0 ie; in years");
            }
            if (request.Amount < 0)
            {
                validationErrors.AddLast("Amount must be greater than zero");
            }

            return validationErrors;
        }
    }
}
