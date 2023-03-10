using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ecommerce.Data.ViewModels
{
    public class NewCartItemModel
    {
        [Required(ErrorMessage = "Phải có id của sản phẩm")]
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int quantity { get; set; }
    }
}
