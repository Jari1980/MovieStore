using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
#nullable disable

namespace Movie_Store.Models
{
    public class Customers
    {

        public int Id { get; set; }

        [Display(Name = "First name")]
        [StringLength(200, MinimumLength = 2)]

        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [StringLength(200, MinimumLength = 2)]

        public string LastName { get; set; }

        [Display(Name = "Billing adress")]
        [StringLength(200, MinimumLength = 2)]
        public string BillingAdress { get; set; }

        [Display(Name = "Billing zip")]
        [StringLength(100, MinimumLength = 2)]
        public string BillingZip { get; set; }

        [Display(Name = "Delivery adress")]
        [StringLength(200, MinimumLength = 2)]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Delivery zip")]
        [StringLength(100, MinimumLength = 2)]
        public string DeliveryZip { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        [StringLength(256)]
        public string EmailAdress { get; set; }

        [Display(Name = "Phone nr")]
        [Phone]
        [StringLength(100)]
        public string PhoneNo { get; set; }



    }
}
