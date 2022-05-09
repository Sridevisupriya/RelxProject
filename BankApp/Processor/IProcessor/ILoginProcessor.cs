using BankApp.Models;

namespace BankApp.Processor.IProcessor
{
    public interface ILoginProcessor
    {
        public ResponseModel Process(Customer customer);
        public string GenerateJSONWebToken(Customer userInfo);
    }
}
