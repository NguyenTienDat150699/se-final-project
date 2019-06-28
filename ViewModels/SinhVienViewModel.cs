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
using System.ComponentModel;
using System.Windows;

namespace ViewModels
{
    public class SinhVienViewModel : BaseViewModel
    {
        private SinhVien sinhVien;
        private QuanHuyen quanHuyen;
        private NganhHoc nganhHoc;

        public SinhVien SinhVien
        {
            get { return sinhVien; }
            set
            {
                if (value != null) sinhVien = value;
                OnPropertyChanged("SinhVien");
            }
        }
        public NganhHoc NganhHoc
        {
            get { return nganhHoc; }
            set
            {
                if (value != null) nganhHoc = value;
                OnPropertyChanged("NganhHoc");
            }
        }
        public QuanHuyen QuanHuyen
        {
            get { return quanHuyen; }
            set
            {
                if (value != null) quanHuyen = value;
                OnPropertyChanged("QuanHuyen");
            }
        }

        public ICommand XacNhan { get; set; }
        private void XacNhanLuuSinhVien()
        {
            sinhVien.QueQuan = quanHuyen.MaQuanHuyen;
            sinhVien.NganhHoc = nganhHoc.MaNganhHoc;
            string errorString = CheckThongTinSinhVien();
            if (errorString == "")
            {
                SinhVienDAL sinhVienDAL = new SinhVienDAL(dbConnection);
                sinhVienDAL.CreateItem(sinhVien);
                MessageBox.Show("Lưu Sinh Viên thành công");
                NhapLaiThongTinSinhVien();
                SetMaSoIfInvalid();
            }
            else
                MessageBox.Show(errorString, "ERROR");
        }
        private void SetMaSoIfInvalid()
        {
            SinhVienDAL sinhVienDAL = new SinhVienDAL(dbConnection);
            while (sinhVienDAL.IsMaSoExisted(sinhVien.MaSo))
                sinhVien.MaSo++;
            OnPropertyChanged("SinhVien");
        }
        private string CheckThongTinSinhVien()
        {
            string invalidProperties = "";
            SetMaSoIfInvalid();
            if (sinhVien.HoTen == "")
                invalidProperties += "\nHọ Tên không hợp lệ";
            if (sinhVien.NganhHoc == 0)
                invalidProperties += "\nNgành Học không hợp lệ";
            if (sinhVien.DiaChi == "")
                invalidProperties += "\nĐịa Chỉ không hợp lệ";
            if (sinhVien.DoiTuong == 0)
                invalidProperties += "\nĐối Tượng không hợp lệ";
            if (sinhVien.QueQuan == 0)
                invalidProperties += "\nQuê Quán không hợp lệ";
            return invalidProperties;
        }

        public ICommand NhapLai { get; set; }
        private void NhapLaiThongTinSinhVien()
        {
            SinhVien = new SinhVien();
            QuanHuyen = new QuanHuyen();
            NganhHoc = new NganhHoc();
        }

        public SinhVienViewModel() : base()
        {
            sinhVien = new SinhVien();
            quanHuyen = new QuanHuyen();
            nganhHoc = new NganhHoc();

            XacNhan = new RelayCommand(
                param => true, param => XacNhanLuuSinhVien());
            NhapLai = new RelayCommand(
                param => true, param => NhapLaiThongTinSinhVien());

            LoadDanhMucTinhThanh();
            LoadDanhMucQuanHuyen();
            LoadDanhMucDTUT();
            LoadDanhMucKhoa();
            LoadDanhMucNganhHoc();
        }

        private void LoadDanhMucTinhThanh()
        {
            TinhThanhDAL tinhThanhDAL = new TinhThanhDAL(dbConnection);
            DanhMucTinhThanh = tinhThanhDAL.ReadAllItems();
        }
        public void LoadDanhMucQuanHuyen()
        {
            QuanHuyenDAL quanHuyenDAL = new QuanHuyenDAL(dbConnection);
            DanhMucQuanHuyen = quanHuyenDAL.ReadItemsByTinhThanh(quanHuyen.TinhThanh);
            OnPropertyChanged("DanhMucQuanHuyen");
        }
        private void LoadDanhMucDTUT()
        {
            DoiTuongUuTienDAL doiTuongUuTienDAL = new DoiTuongUuTienDAL(dbConnection);
            DanhMucDTUT = doiTuongUuTienDAL.ReadAllItems();
        }
        private void LoadDanhMucKhoa()
        {
            KhoaDAL khoaDAL = new KhoaDAL(dbConnection);
            DanhMucKhoa = khoaDAL.ReadAllItems();
        }
        private void LoadDanhMucNganhHoc()
        {
            NganhHocDAL nganhHocDAL = new NganhHocDAL(dbConnection);
            DanhMucNganhHoc = nganhHocDAL.ReadAllItems();
        }

        public List<TinhThanh> DanhMucTinhThanh { get; set; }
        public List<QuanHuyen> DanhMucQuanHuyen { get; set; }
        public List<DoiTuongUuTien> DanhMucDTUT { get; set; }
        public List<Khoa> DanhMucKhoa { get; set; }
        public List<NganhHoc> DanhMucNganhHoc { get; set; }
    }
}
