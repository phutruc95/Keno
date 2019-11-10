using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class SaleCode
    {
        [Key]
        [MaxLength(10)]
        public string Code { get; set; }
        public float Percent { get; set; }
        public DateTime OverduedDate { get; set; }
        
        [MaxLength(250)]
        public string Username { get; set; }
        [ForeignKey("Username")]
        public virtual UserProperty UserProperty { get; set; }
    }
}
