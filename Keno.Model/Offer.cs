using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class Offer
    {
        public int ID { get; set; }
        public float Percent { get; set; }
        public DateTime OverduedDate { get; set; }
        public string Desc { get; set; }
    }
}
