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
    public class CT_ChuongTrinhHocDAL : MSAccessDatabase
    {
        public CT_ChuongTrinhHocDAL(OleDbConnection connection) : base(connection) { }
        public CT_ChuongTrinhHocDAL(string connectionString) : base(connectionString) { }

        public void DeleteItemsByNganhHoc(int maNganhHoc)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "DELETE FROM CT_CHUONG_TRINH_HOC WHERE NganhHoc=@manganhhoc", connection);
                command.Parameters.Add("@manganhhoc", OleDbType.Numeric).Value = maNganhHoc;
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
        public void CreateItem(CT_ChuongTrinhHoc cT_ChuongTrinhHoc)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO CT_CHUONG_TRINH_HOC (NganhHoc, MonHoc, HocKy, GhiChu) " +
                    "VALUES (@nganhhoc, @monhoc, @hocky, @ghichu)",
                    connection);
                command.Parameters.Add("@nganhhoc", OleDbType.Numeric).Value = cT_ChuongTrinhHoc.NganhHoc;
                command.Parameters.Add("@monhoc", OleDbType.Numeric).Value = cT_ChuongTrinhHoc.MonHoc;
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = cT_ChuongTrinhHoc.HocKy;
                command.Parameters.Add("@ghichu", OleDbType.BSTR).Value = cT_ChuongTrinhHoc.GhiChu;

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

        public DataTable ReadItemByNganhHocDataTable(int maNganhHoc)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT HocKy, MonHoc, LoaiMon, SoTinChi, GhiChu " +
                    "FROM CT_CHUONG_TRINH_HOC, MON_HOC " +
                    "WHERE NganhHoc=@manganhhoc AND MaMonHoc=MonHoc " +
                    "ORDER BY HocKy ASC, MonHoc ASC", connection);
                command.Parameters.Add("@manganhhoc", OleDbType.Numeric).Value = maNganhHoc;
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
    }
}
