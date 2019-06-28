using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Khoa
    {
        public Khoa()
        {
            TenKhoa = "";
        }
        public int MaKhoa { get; set; }
        public string TenKhoa { get; set; }
    }
}
