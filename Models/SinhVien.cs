using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SinhVien
    {
        public SinhVien()
        {
            NgaySinh = new DateTime(2000, 01, 01);
        }
        public int MaSo { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public int QueQuan { get; set; }
        public int DoiTuong { get; set; }
        public int NganhHoc { get; set; }
    }
}
