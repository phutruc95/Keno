using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class Offer
    {
        public int ID { get; set; }
        [Display(Name = "Phần trăm")]
        public float Percent { get; set; }
        [Display(Name = "Ngày hết hạn")]
        public DateTime OverduedDate { get; set; }
        [Display(Name = "Mô tả")]
        public string Desc { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Số xu cần đổi")]
        public decimal CoinsConsumed { get; set; }
    }
}
