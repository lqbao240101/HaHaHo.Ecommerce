using Ecommerce.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    [Table("Categories")]
    //[Index(nameof(CategoryName), IsUnique = true)]
    public class Category : IEntityBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [MinLength(2, ErrorMessage = "Tên phải có ít nhất 2 ký tự")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Thông tin không được để trống")]
        [MinLength(10, ErrorMessage = "Thông tin phải có ít nhất 10 ký tự")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        // Một category có nhiều products
       public List<Product> Products { get; set; }
    }
}
