using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Keno.Model
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Display(Name="Tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        public string Url { get; set; }
        [Display(Name = "Giá")]
        public float Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public float SalePrice { get; set; }
        [Display(Name = "Đang khuyến mãi")]
        public bool IsOnSale { get; set; }
        [Display(Name = "Loại sản phẩm")]
        public int ProductTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
