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
    }
}
