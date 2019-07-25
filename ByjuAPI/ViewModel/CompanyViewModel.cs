using System.ComponentModel.DataAnnotations;

namespace ByjuAPI.ViewModel
{
    public class CompanyViewModel
    {
        [Required(ErrorMessage = "Please enter company name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter company address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }
    }
}