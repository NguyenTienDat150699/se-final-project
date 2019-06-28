using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using Models;
using DataAccessLayer;
using System.Windows.Input;
using Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;

namespace ViewModels
{
    public class MonHocViewModel : BaseViewModel
    {
        private MonHoc monHoc;
        private LoaiMon loaiMon;

        public MonHoc MonHoc
        {
            get { return monHoc; }
            set
            {
                if (value != null) monHoc = value;
                OnPropertyChanged("MonHoc");
            }
        }
        public LoaiMon LoaiMon
        {
            get { return loaiMon; }
            set
            {
                if (value != null) loaiMon = value;
                OnPropertyChanged("LoaiMon");
            }
        }

        public ICommand ThemMonHoc { get; set; }
        private void ThemMonHocVaoDanhMucMonHoc()
        {
            monHoc.LoaiMon = loaiMon.MaLoaiMon;
            string errorString = CheckThongTinMonHoc();
            if (errorString == "")
            {
                DanhMucMonHoc.Add(monHoc);
                MonHoc = new MonHoc();
                SetMaSoIfInvalid();
                MessageBox.Show("Thêm Môn Học thành công");
            }
            else
                MessageBox.Show(errorString, "ERROR");
        }
        private void SetMaSoIfInvalid()
        {
            foreach (MonHoc mh in DanhMucMonHoc)
                if (mh.MaMonHoc == monHoc.MaMonHoc)
                    monHoc.MaMonHoc++;
            OnPropertyChanged("MonHoc");
        }
        private string CheckThongTinMonHoc()
        {
            string invalidProperties = "";
            SetMaSoIfInvalid();
            if (monHoc.TenMonHoc == "")
                invalidProperties += "\nTên Môn Học không hợp lệ";
            if (monHoc.LoaiMon == 0)
                invalidProperties += "\nLoại Môn Học không hợp lệ";
            if (monHoc.SoTiet == 0)
                invalidProperties += "\nSố Tiết không hợp lệ";
            if (monHoc.SoTinChi == 0)
                invalidProperties += "\nSố Tín Chỉ không hợp lệ";
            return invalidProperties;
        }

        public ICommand XoaMonHoc { get; set; }

        public ICommand HoanNguyen { get; set; }

        public ICommand XacNhan { get; set; }
        private void XacNhanLuuDanhMucMonHoc()
        {
            MonHocDAL monHocDAL = new MonHocDAL(dbConnection);
            monHocDAL.DeleteAllItems();
            foreach (MonHoc mh in DanhMucMonHoc)
                monHocDAL.CreateItem(mh);
            MessageBox.Show("Lưu Danh sách Môn Học thành công");
        }

        public MonHocViewModel() : base()
        {
            monHoc = new MonHoc();
            loaiMon = new LoaiMon();

            ThemMonHoc = new RelayCommand(
                param => true, param => ThemMonHocVaoDanhMucMonHoc());
            XoaMonHoc = new RelayCommand(
                param => (param as MonHoc) != null, 
                param => DanhMucMonHoc.Remove(param as MonHoc));
            HoanNguyen = new RelayCommand(
                param => true, param => LoadDanhMucMonHoc());
            XacNhan = new RelayCommand(
                param => true, param => XacNhanLuuDanhMucMonHoc());

            LoadDanhMucLoaiMon();
            LoadDanhMucMonHoc();
        }

        public void CalculateSoTinChi()
        {
            int soTietCho1TinChi = loaiMon.SoTietCho1TinChi;
            int soTiet = monHoc.SoTiet;
            int soTinChi = soTiet / soTietCho1TinChi;
            soTiet = soTinChi * soTietCho1TinChi;
            monHoc.SoTiet = soTiet;
            monHoc.SoTinChi = soTinChi;
            OnPropertyChanged("MonHoc");
        }

        private void LoadDanhMucLoaiMon()
        {
            LoaiMonDAL loaiMonDAL = new LoaiMonDAL(dbConnection);
            DanhMucLoaiMon = loaiMonDAL.ReadAllItems();
        }
        private void LoadDanhMucMonHoc()
        {
            MonHocDAL monHocDAL = new MonHocDAL(dbConnection);
            List<MonHoc> monHocs = monHocDAL.ReadAllItems();
            DanhMucMonHoc = new ObservableCollection<MonHoc>(monHocs);
            OnPropertyChanged("DanhMucMonHoc");
        }

        public List<LoaiMon> DanhMucLoaiMon { get; set; }
        public ObservableCollection<MonHoc> DanhMucMonHoc { get; set; }
    }
}
