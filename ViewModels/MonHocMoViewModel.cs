using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using Models;
using DataAccessLayer;
using Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Input;
using System.Data;
using System.Windows;

namespace ViewModels
{
    public class MonHocMoViewModel : BaseViewModel
    {
        private DsMonHocMo dsMonHocMo;

        public DsMonHocMo DsMonHocMo
        {
            get { return dsMonHocMo; }
            set
            {
                if (value != null) dsMonHocMo = value;
                OnPropertyChanged("DsMonHocMo");
            }
        }

        public ICommand XacNhan { get; set; }
        private void XacNhanLuuDsMonHocMo()
        {
            string errorString = "";
            if(dsMonHocMo.HocKy == 0)
            {
                errorString = "\nHọc Kỳ không hợp lệ";
                MessageBox.Show(errorString, "ERROR");
                return;
            }

            List<MonHocMo> monHocMos = new List<MonHocMo>();
            foreach(DataRow row in DanhMucMonHocMo.Rows)
            {
                MonHocMo monHocMo = new MonHocMo();
                int number;
                if (int.TryParse(row["MonHoc"].ToString(), out number))
                    continue;
                monHocMo.MonHoc = number;
                monHocMos.Add(monHocMo);
            }
            errorString = CheckThongTinDsMonHocMo(monHocMos);
            if(errorString == "")
            {
                MonHocMoDAL monHocMoDAL = new MonHocMoDAL(dbConnection);
                monHocMoDAL.DeleteItemsByDs(dsMonHocMo.MaDsMonHocMo);
                foreach (MonHocMo monHocMo in monHocMos)
                    monHocMoDAL.CreateItem(monHocMo);
                MessageBox.Show("Lưu Danh sách Môn Học Mở thành công");
            }
            else
                MessageBox.Show(errorString, "ERROR");
        }
        private string CheckThongTinDsMonHocMo(List<MonHocMo> monHocMos)
        {
            for (int i = 0; i < monHocMos.Count; i++)
            {
                monHocMos[i].DsMonHocMo = dsMonHocMo.MaDsMonHocMo;
                for (int j = i + 1; j < monHocMos.Count; j++)
                    if (monHocMos[i].MonHoc == monHocMos[j].MonHoc)
                        return "\nTrùng Môn học trong Danh Sách Môn Học Mở";
            }
            return "";
        }

        public ICommand NhapLai { get; set; }

        public MonHocMoViewModel() : base()
        {
            XacNhan = new RelayCommand(
                param => true, param => XacNhanLuuDsMonHocMo());
            NhapLai = new RelayCommand(
                param => true, param => LoadDanhMucMonHocMo());

            LoadDanhMucHocKy();
            LoadDanhMucLoaiMon();
            LoadDanhMucMonHoc();
        }

        private void LoadDanhMucHocKy()
        {
            HocKyDAL hocKyDAL = new HocKyDAL(dbConnection);
            DanhMucHocKy = hocKyDAL.ReadAllItems();
        }
        private void LoadDanhMucLoaiMon()
        {
            LoaiMonDAL loaimonDAL = new LoaiMonDAL(dbConnection);
            DanhMucLoaiMon = loaimonDAL.ReadAllItems();
        }
        private void LoadDanhMucMonHoc()
        {
            MonHocDAL monhocDAL = new MonHocDAL(dbConnection);
            DanhMucMonHoc = monhocDAL.ReadAllItems();
        }
        private void LoadDanhMucMonHocMo()
        {
            DsMonHocMoDAL dsMonHocMoDAL = new DsMonHocMoDAL(dbConnection);
            if (!dsMonHocMoDAL.IsExistedByHocKyAndNamHoc(dsMonHocMo))
                dsMonHocMoDAL.CreateItemByHocKyAndNamHoc(dsMonHocMo);
            dsMonHocMo = dsMonHocMoDAL.ReadItemByHocKyAndNamHoc(dsMonHocMo);
            MonHocMoDAL monHocMoDAL = new MonHocMoDAL(dbConnection);
            DanhMucMonHocMo = monHocMoDAL.ReadItemsByDsDataTable(dsMonHocMo.MaDsMonHocMo);
            OnPropertyChanged("DanhMucMonHocMo");
        }

        public List<HocKy> DanhMucHocKy { get; set; }
        public List<LoaiMon> DanhMucLoaiMon { get; set; }
        public List<MonHoc> DanhMucMonHoc { get; set; }
        public DataTable DanhMucMonHocMo { get; set; }
    }
}
