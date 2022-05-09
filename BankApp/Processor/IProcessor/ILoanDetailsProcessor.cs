using BankApp.Models;

namespace BankApp.Processor.IProcessor
{
    public interface ILoanDetailsProcessor<in TSource, out ResponseModel>
    {
        ResponseModel GetByMailId(string mailId);
        ResponseModel ApplyLoan(LoanDetails loanData);
    }
}
