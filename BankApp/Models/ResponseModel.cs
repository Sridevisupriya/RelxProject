using System.Collections.Generic;

namespace BankApp.Models
{
    public class ResponseModel
    {
        public ResponseCode StateOfModel { get; set; }
        public string Data { get; set; }
        public LinkedList<string> Errors { get; set; }
        public IEnumerable<LoanDetails> LoanDetails { get; set; }
        public Customer customer { get; set; }
    }
}
