using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PhieuDKHP
    {
        public PhieuDKHP()
        {
            NgayLap = DateTime.Today;
            ThoiHangDongHP = DateTime.Today;
        }
        public int SoPhieuDKHP { get; set; }
        public DateTime NgayLap { get; set; }
        public int NamHoc { get; set; }
        public int MaSoSV { get; set; }
        public double SoTienDangKy { get; set; }
        public DateTime ThoiHangDongHP { get; set; }
        public double SoTienPhaiDong { get; set; }
        public double SoTienConLai { get; set; }
    }
}
