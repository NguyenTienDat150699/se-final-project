using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.OleDb;
using Models;
using DataAccessLayer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infrastructure;
using System.Windows;

namespace ViewModels
{
    public class PhieuThuHpViewModel : BaseViewModel
    {
        private PhieuThuHP phieuThuHP;
        private SinhVien sinhVien;
        private PhieuDKHP phieuDKHP;

        public PhieuDKHP PhieuDKHP
        {
            get { return phieuDKHP; }
            set
            {
                if (value != null) phieuDKHP = value;
                OnPropertyChanged("PhieuDKHP");
            }
        }
        public PhieuThuHP PhieuThuHP
        {
            get { return phieuThuHP; }
            set
            {
                if (value != null) phieuThuHP = value;
                OnPropertyChanged("PhieuThuHP");
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

        public ICommand XacNhan { get; set; }
        private void XacNhanLuuPhieuThuHP()
        {
            phieuThuHP.PhieuDKHP = phieuDKHP.SoPhieuDKHP;
            string errorString = CheckThongTinPhieuThuHP();
            if (errorString == "")
            {
                PhieuThuHP_DAL phieuThuHP_DAL = new PhieuThuHP_DAL(dbConnection);
                PhieuDKHP_DAL phieuDKHP_DAL = new PhieuDKHP_DAL(dbConnection);
                phieuThuHP_DAL.CreateItem(phieuThuHP);
                phieuDKHP_DAL.UpdateItem(phieuDKHP);
                MessageBox.Show("Lưu Phiếu Thu Học Phí thành công");
            }
            else
                MessageBox.Show(errorString, "ERROR");
        }
        private string CheckThongTinPhieuThuHP()
        {
            string invalidProperties = "";
            SetMaSoIfInvalid();
            PhieuDKHP_DAL phieuDKHP_DAL = new PhieuDKHP_DAL(dbConnection);
            if (!phieuDKHP_DAL.IsMaSoExisted(phieuThuHP.PhieuDKHP))
                invalidProperties += "\nPhiếu ĐKHP không hợp hệ";
            if (phieuThuHP.SoTienThu == 0 && phieuDKHP.SoTienConLai < 0)
                invalidProperties += "\nSố Tiền Thu không hợp hệ";
            return invalidProperties;
        }
        private void SetMaSoIfInvalid()
        {
            PhieuThuHP_DAL phieuThuHP_DAL = new PhieuThuHP_DAL(dbConnection);
            while (phieuThuHP_DAL.IsMaSoExisted(phieuThuHP.SoPhieuThuHP))
                phieuThuHP.SoPhieuThuHP++;
            OnPropertyChanged("PhieuThuHP");
        }

        public ICommand NhapLai { get; set; }
        private void NhapLaiThongTinPhieuThuHP()
        {
            PhieuThuHP = new PhieuThuHP();
            PhieuDKHP = new PhieuDKHP();
            SinhVien = new SinhVien();
        }

        public void CalculateSoTien()
        {
            phieuDKHP.SoTienConLai -= phieuThuHP.SoTienThu;
            OnPropertyChanged("PhieuDKHP");
        }

        public PhieuThuHpViewModel() : base()
        {
            phieuDKHP = new PhieuDKHP();
            sinhVien = new SinhVien();
            phieuThuHP = new PhieuThuHP();

            XacNhan = new RelayCommand(
                param => true, param => XacNhanLuuPhieuThuHP());
            NhapLai = new RelayCommand(
                param => true, param => NhapLaiThongTinPhieuThuHP());

            LoadDanhMucSinhVien();
            LoadDanhMucHocKy();
        }

        private void LoadDanhMucHocKy()
        {
            HocKyDAL hocKyDAL = new HocKyDAL(dbConnection);
            DanhMucHocKy = hocKyDAL.ReadAllItems();
        }
        private void LoadDanhMucSinhVien()
        {
            SinhVienDAL sinhVienDAL = new SinhVienDAL(dbConnection);
            DanhMucSinhVien = sinhVienDAL.ReadAllItems();
        }
        private void LoadDanhMucPhieuDKHP()
        {
            PhieuDKHP_DAL phieuDKHP_DAL = new PhieuDKHP_DAL(dbConnection);
            DanhMucPhieuDKHP = phieuDKHP_DAL.ReadItemsByMSSV(sinhVien.MaSo);
            OnPropertyChanged("DanhMucPhieuDKHP");
        }

        public List<HocKy> DanhMucHocKy { get; set; }
        List<SinhVien> DanhMucSinhVien { get; set; }
        List<PhieuDKHP> DanhMucPhieuDKHP { get; set; }
    }
}
