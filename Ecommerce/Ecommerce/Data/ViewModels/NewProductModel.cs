using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Ecommerce.Data.ViewModels 
{ 

    public class NewProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Thông tin không được để trống")]
        [MinLength(10, ErrorMessage = "Tên phải có ít nhất 10 ký tự")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Column(TypeName = "Money")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải ít nhất là 0 đồng")]
        [DefaultValue(0)]
        public double Price { get; set; }

        [Required(ErrorMessage = "Giảm giá không được để trống")]
        [Range(0, 100, ErrorMessage = "Giảm giá từ 0 - 100 (%)")]
        [DefaultValue(0)]
        public double PercentSale { get; set; }

        [Required(ErrorMessage = "Hình ảnh sản phẩm không được để trống")]
        public string ProductImg { get; set; }


        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải ít nhất là 0")]
        [DefaultValue(0)]
        public int Quantity { get; set; }

        // Một product có 1 category
        [Required(ErrorMessage = "Sản phẩm phải có idCategory")]
        public int CategoryId { get; set; }
    }
}
