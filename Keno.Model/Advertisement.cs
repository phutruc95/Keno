using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class Advertisement
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Chủ đề")]
        public string Title { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Liên kết")]
        public string Url { get; set; }
        [Display(Name = "Sản phẩm")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
