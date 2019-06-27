using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoaiMon
    {
        public LoaiMon()
        {
            TenLoaiMon = "";
            SoTietCho1TinChi = byte.MaxValue;
            SoTienCho1TinChi = 0;
        }
        public int MaLoaiMon { get; set; }
        public string TenLoaiMon { get; set; }
        public int SoTietCho1TinChi { get; set; }
        public double SoTienCho1TinChi { get; set; }
    }
}
