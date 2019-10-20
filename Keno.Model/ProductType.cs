using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class ProductType
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Tên loại sản phẩm")]
        public string TypeName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
