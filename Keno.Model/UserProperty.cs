using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class UserProperty
    {
        [Key]
        [MaxLength(250)]
        public string Username { get; set; }
        [MaxLength(7)]
        public string AttendanceList { get; set; }
        public decimal Coins { get; set; }
        public virtual List<SaleCode> SaleCodes { get; set; }
    }
}
