using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class QuanHuyen
    {
        public QuanHuyen()
        {
            TenQuanHuyen = "";
            ThongTin = "";
        }
        public int MaQuanHuyen { get; set; }
        public int TinhThanh { get; set; }
        public string TenQuanHuyen { get; set; }
        public string ThongTin { get; set; }
    }
}
