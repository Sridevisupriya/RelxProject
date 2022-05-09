using BankApp.Models;

namespace BankApp.Repository.IRepository
{
    public interface ILoginRepository
    {
        bool Login(Customer customer);
        Customer Register(Customer customer);
    }
}
