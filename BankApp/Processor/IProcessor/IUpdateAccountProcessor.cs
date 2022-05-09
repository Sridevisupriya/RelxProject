using BankApp.Models;

namespace BankApp.Processor.IProcessor
{
    public interface IUpdateAccountProcessor<in TSource, out ResponseModel>
    {
        ResponseModel UpdateAccountDetails(Customer customer);

    }
}
