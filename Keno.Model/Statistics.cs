using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keno.Model
{
    public class Statistics
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Accessions { get; set; }
        public DateTime AccessDate { get; set; }
        public int Month
        {
            get
            {
                return AccessDate.Month;
            }
        }
        public int Day
        {
            get
            {
                return AccessDate.Day;
            }
        }
    }
}
