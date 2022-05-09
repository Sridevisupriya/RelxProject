using BankApp.Models;
using BankApp.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Repository
{
    public class LoanDetailsRepository : ILoanDetailsRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly ILogger<LoanDetailsRepository> _logger;
        public LoanDetailsRepository(CustomerDbContext customerDbContext, ILogger<LoanDetailsRepository> logger)
        {
            _customerDbContext = customerDbContext;
            _logger = logger;
        }

        public LoanDetails ApplyLoan(LoanDetails loanData)
        {
        
            var user = _customerDbContext.Customers.FirstOrDefault(x => x.MailId == loanData.MailId);
            if (user == null)
            {
                _logger.LogInformation("User does'nt exist in the Database");
                return null;
            }
            else
            {
                _logger.LogInformation("Loan details are inserted into Database");
                _customerDbContext.Add(loanData);
                _customerDbContext.SaveChanges();
                return loanData;
            }
        }

        public IEnumerable<LoanDetails> GetByMailId(string mailId)
        {
           var loan = _customerDbContext.LoanDetails.Where(x => x.MailId == mailId).ToList();           
            if(loan.Count>0)
            {
                return loan;
            }
            return null;
        }
    }
}
