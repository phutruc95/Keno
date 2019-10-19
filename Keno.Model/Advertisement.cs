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
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
    }
}
