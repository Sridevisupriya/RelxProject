using System;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Models
{
    public class LoanDetails
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string MailId { get; set; }

        [Required]
        public string LoanType { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double RateOfInterest { get; set; }

        [Required]
        [Display(Name = "LoanDuration(No of Yrs)")]
        public int DurationOfLoan { get; set; }

    }
}
