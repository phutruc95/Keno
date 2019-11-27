using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class AppSetting
    {
        [MaxLength(20)]
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
