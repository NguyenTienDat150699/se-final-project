using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PhieuDKHP_DAL : MSAccessDatabase
    {
        public PhieuDKHP_DAL(OleDbConnection connection) : base(connection) { }
        public PhieuDKHP_DAL(string connectionString) : base(connectionString) { }

        public void CreateItem(PhieuDKHP phieuDKHP)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO PHIEU_DKHP " +
                    "(SoPhieuDKHP, NgayLap, HocKy, NamHoc, MaSoSV, SoTienDangKy, ThoiHanDongHP, SoTienPhaiDong, SoTienConLai) " +
                    "VALUES (@sophieu, @ngaylap, @hocky, @namhoc, @mssv, @sotiendk, @thoihan, @sotienpd, @sotiencl)",
                    connection);
                command.Parameters.Add("@sophieu", OleDbType.Numeric).Value = phieuDKHP.SoPhieuDKHP;
                command.Parameters.Add("@ngaylap", OleDbType.Date).Value = phieuDKHP.NgayLap;
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = phieuDKHP.HocKy;
                command.Parameters.Add("@namhoc", OleDbType.Numeric).Value = phieuDKHP.NamHoc;
                command.Parameters.Add("@mssv", OleDbType.Numeric).Value = phieuDKHP.MaSoSV;
                command.Parameters.Add("@sotiendk", OleDbType.Currency).Value = phieuDKHP.SoTienDangKy;
                command.Parameters.Add("@thoihan", OleDbType.Date).Value = phieuDKHP.ThoiHangDongHP;
                command.Parameters.Add("@sotienpd", OleDbType.Currency).Value = phieuDKHP.SoTienPhaiDong;
                command.Parameters.Add("@sotiencl", OleDbType.Currency).Value = phieuDKHP.SoTienConLai;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        
        public DataTable BaoCaoSinhVienChuaHoanThanhHP(int maHocKy, int namHoc)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT MaSo, HoTen, SoTienDangKy, SoTienPhaiDong, SoTienConLai " +
                    "FROM SINH_VIEN, PHIEU_DKHP " +
                    "WHERE MaSo=MaSoSV AND HocKy=@hocky AND NamHoc=@namhoc AND SoTienConLai>0 " +
                    "ORDER BY MaSo ASC, HoTen ASC", connection);
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = maHocKy;
                command.Parameters.Add("@namhoc", OleDbType.Numeric).Value = namHoc;
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
                oleDbDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }

        public void UpdateSoTienConLai(PhieuDKHP phieuDKHP)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "UPDATE PHIEU_DKHP SET SoTienConLai=@sotiencl WHERE SoPhieuDKHP=@maso",
                    connection);
                command.Parameters.Add("@sotiencl", OleDbType.Numeric).Value = phieuDKHP.SoTienConLai;
                command.Parameters.Add("@maso", OleDbType.Currency).Value = phieuDKHP.SoPhieuDKHP;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool IsMaSoExisted(int soPhieuDKHP)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT COUNT(SoPhieuDKHP) FROM PHIEU_DKHP WHERE SoPhieuDKHP=@maso", connection);
                command.Parameters.Add("@maso", OleDbType.Numeric).Value = soPhieuDKHP;
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
                oleDbDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return int.Parse(dataTable.Rows[0][0].ToString()) == 0 ? false : true;
        }

        public List<PhieuDKHP> ReadItemsByMSSV(int maSoSV)
        {
            List<PhieuDKHP> phieuDKHPs = new List<PhieuDKHP>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM PHIEU_DKHP WHERE MaSoSV=@mssv " +
                    "ORDER BY HocKy ASC, NamHoc ASC", connection);
                command.Parameters.Add("@mssv", OleDbType.Numeric).Value = maSoSV;
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
                oleDbDataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            foreach (DataRow row in dataTable.Rows)
            {
                PhieuDKHP phieuDKHP = new PhieuDKHP();
                phieuDKHP.SoPhieuDKHP = int.Parse(row["SoPhieuDKHP"].ToString());
                phieuDKHP.NgayLap = DateTime.Parse(row["NgayLap"].ToString());
                phieuDKHP.ThoiHangDongHP = DateTime.Parse(row["ThoiHanDongHP"].ToString());
                phieuDKHP.HocKy = int.Parse(row["HocKy"].ToString());
                phieuDKHP.NamHoc = int.Parse(row["NamHoc"].ToString());
                phieuDKHP.MaSoSV = maSoSV;
                phieuDKHP.SoTienDangKy = double.Parse(row["SoTienDangKy"].ToString());
                phieuDKHP.SoTienPhaiDong = double.Parse(row["SoTienPhaiDong"].ToString());
                phieuDKHP.SoTienConLai = double.Parse(row["SoTienConLai"].ToString());
                phieuDKHPs.Add(phieuDKHP);
            }
            return phieuDKHPs;
        }
    }
}
