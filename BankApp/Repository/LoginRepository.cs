using BankApp.Models;
using BankApp.Repository.IRepository;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace BankApp.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly CustomerDbContext _customerDbContext;
        private readonly ILogger<LoginRepository> _logger;
        public LoginRepository(CustomerDbContext customerDbContext, ILogger<LoginRepository> logger)
        {
            _customerDbContext = customerDbContext;
            _logger = logger;
        }

        bool ILoginRepository.Login(Customer customer)
        {
            var user = _customerDbContext.Customers.FirstOrDefault(x => x.MailId == customer.MailId && x.Password == customer.Password);
            if (user == null)
            {
                _logger.LogInformation("User does'nt exist in the database / Password incorrect");
                return false;
            }
            else
            {
                _logger.LogInformation("Login details checked in Database , successfull");
                return true;
            }

        }

        Customer ILoginRepository.Register(Customer customer)
        {
            var user = _customerDbContext.Customers.FirstOrDefault(x => x.MailId == customer.MailId);

            if (user == null)
            {
                _logger.LogInformation("User details inserted into Database");
                _customerDbContext.Add(customer);
                _customerDbContext.SaveChanges();
                var userDetails = _customerDbContext.Customers.FirstOrDefault(x => x.MailId == customer.MailId);
                return userDetails;
            }
            else
            {
                _logger.LogError("Registration unsuccessfull");
                return null;
            }
        }
    }
}
