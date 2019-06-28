using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DsMonHocMo
    {
        public DsMonHocMo()
        {
            NamHoc = 2000;
        }
        public int MaDsMonHocMo { get; set; }
        public int HocKy { get; set; }
        public int NamHoc { get; set; }
    }
}
