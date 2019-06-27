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
using System.Windows.Input;
using System.Data;
using System.Windows;

namespace ViewModels
{
    public class ChuongTrinhHocViewModel : BaseViewModel
    {
        private NganhHoc nganhHoc;
        
        public NganhHoc NganhHoc
        {
            get { return nganhHoc; }
            set
            {
                if (value != null) nganhHoc = value;
                OnPropertyChanged("NganhHoc");
            }
        }
        public DataRowView SelectedRow { get; set; }

        public ICommand XacNhan { get; set; }
        private void XacNhanLuuChuongTrinhHoc()
        {
            string errorString = "";
            if(nganhHoc.MaNganhHoc == 0)
            {
                errorString = "\nNgành Học không hợp lệ";
                MessageBox.Show(errorString, "ERROR");
                return;
            }

            NganhHocDAL nganhHocDAL = new NganhHocDAL(dbConnection);
            List<CT_ChuongTrinhHoc> cT_ChuongTrinhHocs = new List<CT_ChuongTrinhHoc>();
            foreach (DataRow row in CT_ChuongTrinhHocs.Rows)
            {
                CT_ChuongTrinhHoc cT_ChuongTrinhHoc = new CT_ChuongTrinhHoc();
                cT_ChuongTrinhHoc.NganhHoc = nganhHoc.MaNganhHoc;
                int number;
                if (!int.TryParse(row["HocKy"].ToString(), out number))
                    throw new Exception();
                cT_ChuongTrinhHoc.HocKy = number;
                if (!int.TryParse(row["MonHoc"].ToString(), out number))
                    continue;
                cT_ChuongTrinhHoc.MonHoc = number;
                cT_ChuongTrinhHoc.GhiChu = row["GhiChu"].ToString();
                cT_ChuongTrinhHocs.Add(cT_ChuongTrinhHoc);
            }
            errorString = CheckThongTinChuongTrinhHoc(cT_ChuongTrinhHocs);
            if (errorString == "")
            {
                CT_ChuongTrinhHocDAL cT_ChuongTrinhHocDAL = new CT_ChuongTrinhHocDAL(dbConnection);
                cT_ChuongTrinhHocDAL.DeleteItemsByNganhHoc(nganhHoc.MaNganhHoc);
                foreach (CT_ChuongTrinhHoc cT_ChuongTrinhHoc in cT_ChuongTrinhHocs)
                    cT_ChuongTrinhHocDAL.CreateItem(cT_ChuongTrinhHoc);
                if(CT_ChuongTrinhHocs.Rows.Count > cT_ChuongTrinhHocs.Count)
                    MessageBox.Show("Có những Môn Học chưa đầy đủ thông tin bị bỏ qua", "WARNNING");
                MessageBox.Show("Lưu Chương Trình Học thành công");
            }
            else
                MessageBox.Show(errorString, "ERROR");
        }
        private string CheckThongTinCtChuongTrinhHoc(CT_ChuongTrinhHoc cT_ChuongTrinhHoc)
        {
            string invalidProperties = "";
            MonHocDAL monHocDAL = new MonHocDAL(dbConnection);
            if (!monHocDAL.IsMaSoExisted(cT_ChuongTrinhHoc.MonHoc))
                invalidProperties += "\nMôn Học không hợp lệ";
            return invalidProperties;
        }
        private string CheckThongTinChuongTrinhHoc(List<CT_ChuongTrinhHoc> cT_ChuongTrinhHocs)
        {
            string invalidProperties = "";
            for(int i = 0; i < cT_ChuongTrinhHocs.Count; i++)
            {
                invalidProperties = CheckThongTinCtChuongTrinhHoc(cT_ChuongTrinhHocs[i]);
                if (invalidProperties != "")
                    return invalidProperties;
                for(int j = i + 1; j < cT_ChuongTrinhHocs.Count; j++)
                    if(cT_ChuongTrinhHocs[i].MonHoc == cT_ChuongTrinhHocs[j].MonHoc)
                        return "\nTrùng Môn Học trong Chương Trình Học";
            }
            return invalidProperties;
        }

        public ICommand NhapLai { get; set; }

        public ICommand ThemDong { get; set; }
        private void ThemDongCT_ChuongTrinhHoc()
        {
            DataRow dataRow = CT_ChuongTrinhHocs.NewRow();
            CT_ChuongTrinhHocs.Rows.Add(dataRow);
        }

        public ICommand SelectedMonHocChanged { get; set; }
        private void OnSelectedMonHocChanged(object mamonhoc)
        {
            if (mamonhoc == null) return;
            DataRow dataRow = SelectedRow.Row;
            foreach (MonHoc monHoc in DanhMucMonHoc)
            {
                if (monHoc.MaMonHoc != int.Parse(mamonhoc.ToString()))
                    continue;
                dataRow["MonHoc"] = mamonhoc;
                dataRow["LoaiMon"] = monHoc.LoaiMon;
                dataRow["SoTinChi"] = monHoc.SoTinChi;
            }
        }

        public ChuongTrinhHocViewModel() : base()
        {
            nganhHoc = new NganhHoc();

            XacNhan = new RelayCommand(
                param => true, param => XacNhanLuuChuongTrinhHoc());
            NhapLai = new RelayCommand(
                param => true, param => LoadCT_ChuongTrinhHocs());
            SelectedMonHocChanged = new RelayCommand(
                param => true, param => OnSelectedMonHocChanged(param));
            ThemDong = new RelayCommand(
                param => CT_ChuongTrinhHocs != null,
                param => ThemDongCT_ChuongTrinhHoc());

            LoadDanhMucKhoa();
            LoadDanhMucNganhHoc();
            LoadDanhMucLoaiMon();
            LoadDanhMucMonHoc();
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
        private void LoadCT_ChuongTrinhHocs()
        {
            CT_ChuongTrinhHocDAL cT_ChuongTrinhHocDAL = new CT_ChuongTrinhHocDAL(dbConnection);
            CT_ChuongTrinhHocs = cT_ChuongTrinhHocDAL.ReadItemByNganhHocDataTable(nganhHoc.MaNganhHoc);
            OnPropertyChanged("CT_ChuongTrinhHocs");
        }

        public List<Khoa> DanhMucKhoa { get; set; }
        public List<NganhHoc> DanhMucNganhHoc { get; set; }
        public List<LoaiMon> DanhMucLoaiMon { get; set; }
        public List<MonHoc> DanhMucMonHoc { get; set; }
        public DataTable CT_ChuongTrinhHocs { get; set; }
    }
}
