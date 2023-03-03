using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ecommerce.Data.ViewModels
{
    public class UpdateCartItemModel
    {
        [Required(ErrorMessage = "Phải có id của khách hàng")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Phải có id của sản phẩm")]
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int quantity { get; set; }
    }
}
