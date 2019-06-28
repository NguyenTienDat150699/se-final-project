using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using Models;
using DataAccessLayer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

namespace ViewModels
{
    public class BaoCaoNghiaVuHpViewModel : BaseViewModel
    {
        public HocKy HocKy { get; set; }
        public int NamHoc { get; set; }

        public BaoCaoNghiaVuHpViewModel() : base()
        {
            HocKy = new HocKy();
            NamHoc = 2000;

            LoadDanhMucHocKy();
        }

        private void LoadDanhMucHocKy()
        {
            HocKyDAL hocKyDAL = new HocKyDAL(dbConnection);
            DanhMucHocKy = hocKyDAL.ReadAllItems();
        }
        public void LoadBangBaoCao()
        {
            PhieuDKHP_DAL phieuDKHP_DAL = new PhieuDKHP_DAL(dbConnection);
            BangBaoCao = phieuDKHP_DAL.BaoCaoSinhVienChuaHoanThanhHP(HocKy.MaHocKy, NamHoc);
            OnPropertyChanged("BangBaoCao");
        }

        public List<HocKy> DanhMucHocKy { get; set; }
        public DataTable BangBaoCao { get; set; }
    }
}
