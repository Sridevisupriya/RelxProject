using System;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string MailId { get; set; }
        public string PAN { get; set; }

        [Required]
        public string Contact { get; set; }
        public DateTime DOB { get; set; }

        [Required]
        public string AccountType { get; set; }

    }
}
