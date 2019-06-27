using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CT_ChuongTrinhHoc
    {
        public CT_ChuongTrinhHoc()
        {
            GhiChu = "";
        }
        public int NganhHoc { get; set; }
        public int MonHoc { get; set; }
        public int HocKy { get; set; }
        public string GhiChu { get; set; }
    }
}
