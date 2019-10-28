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
        public decimal Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        public decimal SalePrice { get; set; }
        [Display(Name = "Đang khuyến mãi")]
        public bool IsOnSale { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public int ProductTypeID { get; set; }
        public virtual ProductType ProductType { get; set; }
        [Display(Name = "Mô tả")]
        public string ShortDesc { get; set; }
        [Display(Name = "Thông tin chi tiết")]
        public string Desc { get; set; }
        private string _rate { get; set; }
        public float Rate
        {
            get
            {
                if (string.IsNullOrEmpty(_rate)) return 0;

                float sum = 0;
                string[] rates = _rate.Split(';');
                foreach (var rate in rates)
                {
                    sum += float.Parse(rate);
                }
                return sum / rates.Length;
            }
            set
            {
                if (string.IsNullOrEmpty(_rate))
                {
                    _rate = value.ToString();
                }
                else
                {
                    _rate += ";" + value.ToString();
                }
            }
        }
    }
}
