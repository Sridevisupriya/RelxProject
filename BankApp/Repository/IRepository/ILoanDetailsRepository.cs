using BankApp.Models;
using System.Collections.Generic;

namespace BankApp.Repository.IRepository
{
    public interface ILoanDetailsRepository
    {
        IEnumerable<LoanDetails> GetByMailId(string mailId);
        LoanDetails ApplyLoan(LoanDetails loanData);
    }
}
