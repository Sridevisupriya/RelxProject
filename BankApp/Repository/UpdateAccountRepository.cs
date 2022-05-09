using BankApp.Models;
using BankApp.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BankApp.Repository
{
    public class UpdateAccountRepository : IUpdateAccountRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly ILogger<UpdateAccountRepository> _logger;
        public UpdateAccountRepository(CustomerDbContext customerDbContext, ILogger<UpdateAccountRepository> logger)
        {
            _customerDbContext = customerDbContext;
            _logger = logger;
        }
        public Customer UpdateAccountDetails(Customer customer)
        {
            var user = _customerDbContext.Customers.FirstOrDefault(x => x.MailId == customer.MailId);
            if (user == null)
            {
                _logger.LogInformation("Customer does'nt exists in Database to perform updation");
                return null;
            }
            else
            {
                user.AccountType = customer.AccountType;
                user.CustomerName = customer.CustomerName;
                user.Contact = customer.Contact;
                user.Address = customer.Address;
                user.PAN = customer.PAN;
                user.State = customer.State;
                user.Country = customer.Country;

                _customerDbContext.SaveChanges();
                var updatedUser = _customerDbContext.Customers.FirstOrDefault(x => x.MailId == customer.MailId);
                return updatedUser;
            }
        }
    }
}
