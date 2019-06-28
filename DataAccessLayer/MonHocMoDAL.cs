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
    public class MonHocMoDAL : MSAccessDatabase
    {
        public MonHocMoDAL(OleDbConnection connection) : base(connection) { }
        public MonHocMoDAL(string connectionString) : base(connectionString) { }

        public void CreateItem(MonHocMo monHocMo)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO MON_HOC_MO (DsMonHocMo, MonHoc) VALUES (@mads, @monhoc)",
                    connection);
                command.Parameters.Add("@mads", OleDbType.Numeric).Value = monHocMo.DsMonHocMo;
                command.Parameters.Add("@monhoc", OleDbType.Numeric).Value = monHocMo.MonHoc;

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

        public void DeleteItemsByDs(int maDsMonHocMo)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "DELETE FROM MON_HOC_MO WHERE DsMonHocMo=@mads", connection);
                command.Parameters.Add("@mads", OleDbType.Numeric).Value = maDsMonHocMo;
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

        public DataTable ReadItemsByDsDataTable(int maDsMonHocMo)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT MonHoc, LoaiMon, SoTinChi " +
                    "FROM MON_HOC_MO, MON_HOC " +
                    "WHERE DsMonHocMo=@mads AND MaMonHoc=MonHoc " +
                    "ORDER BY MonHoc ASC", connection);
                command.Parameters.Add("@mads", OleDbType.Numeric).Value = maDsMonHocMo;
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

        public List<MonHoc> ReadMonHocsByHocKyAndNamHoc(int hocKy, int namHoc)
        {
            List<MonHoc> monHocs = new List<MonHoc>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT MaMonHoc, TenMonHoc, LoaiMon, SoTiet, SoTinChi " +
                    "FROM MON_HOC, MON_HOC_MO, DS_MON_HOC_MO " +
                    "WHERE MaMonHoc=MonHoc AND MaDsMonHocMo=DsMonHocMo " +
                    "AND HocKy=@hocky AND NamHoc=@namhoc " +
                    "ORDER BY MaMonHoc ASC", connection);
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(command);
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = hocKy;
                command.Parameters.Add("@namhoc", OleDbType.Numeric).Value = namHoc;

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
                MonHoc monHoc = new MonHoc();
                monHoc.MaMonHoc = int.Parse(row["MaMonHoc"].ToString());
                monHoc.TenMonHoc = row["TenMonHoc"].ToString();
                monHoc.LoaiMon = int.Parse(row["LoaiMon"].ToString());
                monHoc.SoTiet = int.Parse(row["SoTiet"].ToString());
                monHoc.SoTinChi = int.Parse(row["SoTinChi"].ToString());
                monHocs.Add(monHoc);
            }
            return monHocs;
        }
    }
}
