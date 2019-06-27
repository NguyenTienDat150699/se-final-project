using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PhieuThuHP
    {
        public PhieuThuHP()
        {
            NgayLap = DateTime.Today;
            SoTienThu = 0;
        }
        public int SoPhieuThuHP { get; set; }
        public int PhieuDKHP { get; set; }
        public DateTime NgayLap { get; set; }
        public double SoTienThu { get; set; }
    }
}
