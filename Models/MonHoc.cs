using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MonHoc
    {
        public MonHoc()
        {
            TenMonHoc = "";
        }
        public int MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int LoaiMon { get; set; }
        public int SoTiet { get; set; }
        public int SoTinChi { get; set; }
    }
}
