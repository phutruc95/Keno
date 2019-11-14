using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Keno.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayedName
        {
            get
            {
                Id = Id.Trim();
                switch (Id)
                {
                    case "1": return "Khách";
                    case "2": return "Quản trị viên";
                    case "3": return "Người bán";
                    case "4": return "Quản trị viên tổng";
                    default: return "Vai trò chưa đặt tên";
                }
            }
        }
        public bool IsGranted { get; set; }
    }
}