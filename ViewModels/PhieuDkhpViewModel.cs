using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using Models;
using DataAccessLayer;
using Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Windows.Input;
using System.Windows;

namespace ViewModels
{
    public class PhieuDkhpViewModel : BaseViewModel
    {
        private PhieuDKHP phieuDKHP;
        private SinhVien sinhVien;
        private DoiTuongUuTien doiTuongUuTien;

        public PhieuDKHP PhieuDKHP
        {
            get { return phieuDKHP; }
            set
            {
                if (value != null) phieuDKHP = value;
                OnPropertyChanged("PhieuDKHP");
            }
        }
        public SinhVien SinhVien
        {
            get { return sinhVien; }
            set
            {
                if (value != null) sinhVien = value;
                OnPropertyChanged("SinhVien");
            }
        }
        public DoiTuongUuTien DoiTuongUuTien
        {
            get { return doiTuongUuTien; }
            set
            {
                if (value != null) doiTuongUuTien = value;
                OnPropertyChanged("DoiTuongUuTien");
            }
        }
        
        public ICommand XacNhan { get; set; }
        private void XacNhanLuuPhieuDKHP()
        {
            phieuDKHP.MaSoSV = sinhVien.MaSo;
            string errorString = CheckThongTinPhieuDKHP();
            if (errorString != "")
            {
                MessageBox.Show(errorString, "ERROR");
                return;
            }
            List<CT_PhieuDKHP> cT_PhieuDKHPs = new List<CT_PhieuDKHP>();
            foreach (DataRow row in CT_PhieuDKHPs.Rows)
            {
                CT_PhieuDKHP cT_PhieuDKHP = new CT_PhieuDKHP();
                int number;
                if (!int.TryParse(row["MonHoc"].ToString(), out number))
                    continue;
                cT_PhieuDKHP.MonHoc = number;
                cT_PhieuDKHPs.Add(cT_PhieuDKHP);
            }
            errorString = CheckCT_PhieuDKHPs(cT_PhieuDKHPs);
            if (errorString == "")
            {
                PhieuDKHP_DAL phieuDKHP_DAL = new PhieuDKHP_DAL(dbConnection);
                CT_PhieuDKHP_DAL cT_PhieuDKHP_DAL = new CT_PhieuDKHP_DAL(dbConnection);
                phieuDKHP_DAL.CreateItem(phieuDKHP);
                foreach (CT_PhieuDKHP cT_PhieuDKHP in cT_PhieuDKHPs)
                    cT_PhieuDKHP_DAL.CreateItem(cT_PhieuDKHP);
                MessageBox.Show("Lưu Phiếu ĐKHP thành công");
            }
            else
                MessageBox.Show(errorString, "ERROR");
        }
        private string CheckThongTinPhieuDKHP()
        {
            string invalidProperties = "";
            SetMaSoIfInvalid();
            SinhVienDAL sinhVienDAL = new SinhVienDAL(dbConnection);
            if (phieuDKHP.HocKy == 0)
                invalidProperties += "\nHọc Kỳ không hợp lệ";
            if (!sinhVienDAL.IsMaSoExisted(phieuDKHP.MaSoSV))
                invalidProperties += "\nSinh Viên không hợp lệ";
            if (phieuDKHP.SoTienDangKy == 0)
                invalidProperties += "\nKhông có Môn Học nào được đăng ký";
            if (phieuDKHP.ThoiHangDongHP <= phieuDKHP.NgayLap)
                invalidProperties += "\nThời Hạn không hợp lệ";
            return invalidProperties;
        }
        private string CheckCT_PhieuDKHPs(List<CT_PhieuDKHP> cT_PhieuDKHPs)
        {
            for (int i = 0; i < cT_PhieuDKHPs.Count; i++)
            {
                cT_PhieuDKHPs[i].SoPhieu = phieuDKHP.SoPhieuDKHP;
                for (int j = i + 1; j < cT_PhieuDKHPs.Count; j++)
                    if (cT_PhieuDKHPs[i].MonHoc == cT_PhieuDKHPs[j].MonHoc)
                        return "\nTrùng Môn Học trong Chi tiết Phiếu ĐKHP";
            }
            return "";
        }
        private void SetMaSoIfInvalid()
        {
            PhieuDKHP_DAL phieuDKHP_DAL = new PhieuDKHP_DAL(dbConnection);
            while (!phieuDKHP_DAL.IsMaSoExisted(phieuDKHP.SoPhieuDKHP))
                phieuDKHP.SoPhieuDKHP++;
            OnPropertyChanged("PhieuDKHP");
        }

        public ICommand NhapLai { get; set; }
        private void NhapLaiThongTinPhieuDKHP()
        {
            PhieuDKHP = new PhieuDKHP();
            SinhVien = new SinhVien();
            DoiTuongUuTien = new DoiTuongUuTien();
            LoadDanhMucMHM();
            NewCtPhieuDKHPs();
        }

        public void CalculateSoTien()
        {
            phieuDKHP.SoTienDangKy = 0;
            foreach (DataRow row in CT_PhieuDKHPs.Rows)
            {
                int soTien;
                if (!int.TryParse(row["SoTien"].ToString(), out soTien))
                    continue;
                phieuDKHP.SoTienDangKy += soTien;
            }
            phieuDKHP.SoTienPhaiDong = phieuDKHP.SoTienDangKy * doiTuongUuTien.TiLeMienGiam;
            phieuDKHP.SoTienConLai = phieuDKHP.SoTienPhaiDong;
            OnPropertyChanged("PhieuDKHP");
        }

        public PhieuDkhpViewModel() : base()
        {
            phieuDKHP = new PhieuDKHP();
            sinhVien = new SinhVien();
            doiTuongUuTien = new DoiTuongUuTien();

            XacNhan = new RelayCommand(
                param => true, param => XacNhanLuuPhieuDKHP());
            NhapLai = new RelayCommand(
                param => true, param => NhapLaiThongTinPhieuDKHP());

            LoadDanhMucSinhVien();
            LoadDanhMucHocKy();
            LoadDanhMucDTUT();
            LoadDanhMucNganhHoc();
        }

        private void LoadDanhMucSinhVien()
        {
            SinhVienDAL sinhVienDAL = new SinhVienDAL(dbConnection);
            DanhMucSinhVien = sinhVienDAL.ReadAllItems();
        }
        private void LoadDanhMucNganhHoc()
        {
            NganhHocDAL nganhHocDAL = new NganhHocDAL(dbConnection);
            DanhMucNganhHoc = nganhHocDAL.ReadAllItems();
        }
        private void LoadDanhMucDTUT()
        {
            DoiTuongUuTienDAL doiTuongUuTienDAL = new DoiTuongUuTienDAL(dbConnection);
            DanhMucDTUT = doiTuongUuTienDAL.ReadAllItems();
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
        private void LoadDanhMucMHM()
        {
            MonHocMoDAL monHocMoDAL = new MonHocMoDAL(dbConnection);
            DanhMucMHM = monHocMoDAL.ReadMonHocsByHocKyAndNamHoc(phieuDKHP.HocKy, phieuDKHP.NamHoc);
            OnPropertyChanged("DanhMucMHM");
            NewCtPhieuDKHPs();
        }
        private void NewCtPhieuDKHPs()
        {
            CT_PhieuDKHPs = new DataTable();
            CT_PhieuDKHPs.Columns.Add("MonHoc");
            CT_PhieuDKHPs.Columns.Add("LoaiMon");
            CT_PhieuDKHPs.Columns.Add("SoTinChi");
            CT_PhieuDKHPs.Columns.Add("SoTien");
            OnPropertyChanged("CT_PhieuDKHPs");
        }

        public List<SinhVien> DanhMucSinhVien { get; set; }
        public List<NganhHoc> DanhMucNganhHoc { get; set; }
        public List<DoiTuongUuTien> DanhMucDTUT { get; set; }
        public List<HocKy> DanhMucHocKy { get; set; }
        public List<LoaiMon> DanhMucLoaiMon { get; set; }
        public List<MonHoc> DanhMucMHM { get; set; }
        public DataTable CT_PhieuDKHPs { get; set; }
    }
}
