using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Data.ViewModels
{
    public class NewAddressModel
    {
        [Required(ErrorMessage = "Tên đường không được để trống")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Tên phường/xã không được để trống")]
        public string Ward { get; set; }
        [Required(ErrorMessage = "Tên quận/huyện không được để trống")]
        public string District { get; set; }
        [Required(ErrorMessage = "Tên thành phố/tỉnh không được để trống")]
        public string City { get; set; }
    }
}
