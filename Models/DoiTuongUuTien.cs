using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DoiTuongUuTien
    {
        public DoiTuongUuTien()
        {
            TenDTUT = "";
            TiLeMienGiam = 0;
        }
        public int MaDTUT { get; set; }
        public string TenDTUT { get; set; }
        public double TiLeMienGiam { get; set; }
    }
}
