using BankApp.Models;

namespace BankApp.Repository.IRepository
{
    public interface IUpdateAccountRepository
    {
        Customer UpdateAccountDetails(Customer customer);
    }
}
