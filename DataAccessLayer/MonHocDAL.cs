using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MonHocDAL : MSAccessDatabase
    {
        public MonHocDAL(OleDbConnection connection) : base(connection) { }
        public MonHocDAL(string connectionString) : base(connectionString) { }

        public List<MonHoc> ReadAllItems()
        {
            List<MonHoc> monHocs = new List<MonHoc>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM MON_HOC ORDER BY MaMonHoc ASC", connection);
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
                MonHoc monHoc = new MonHoc();
                monHoc.MaMonHoc = int.Parse(row["MaMonHoc"].ToString());
                monHoc.TenMonHoc = row["TenMonHoc"].ToString();
                monHoc.LoaiMon = int.Parse(row["LoaiMon"].ToString());
                monHoc.SoTiet = byte.Parse(row["SoTiet"].ToString());
                monHoc.SoTinChi = byte.Parse(row["SoTinChi"].ToString());
                monHocs.Add(monHoc);
            }
            return monHocs;
        }

        public void DeleteAllItems()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand("DELETE FROM MON_HOC", connection);
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

        public void CreateItem(MonHoc monHoc)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO MON_HOC (MaMonHoc, TenMonHoc, LoaiMon, SoTiet, SoTinChi) " +
                    "VALUES (@mamh, @tenmh, @loaimon, @sotiet, @sotinchi)",
                    connection);
                command.Parameters.Add("@mamh", OleDbType.Numeric).Value = monHoc.MaMonHoc;
                command.Parameters.Add("@tenmh", OleDbType.BSTR).Value = monHoc.TenMonHoc;
                command.Parameters.Add("@loaimon", OleDbType.Numeric).Value = monHoc.LoaiMon;
                command.Parameters.Add("@sotiet", OleDbType.Numeric).Value = monHoc.SoTiet;
                command.Parameters.Add("@sotinchi", OleDbType.Numeric).Value = monHoc.SoTinChi;

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

        public bool IsMaSoExisted(int maMonHoc)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT COUNT(MaMonHoc) FROM MON_HOC WHERE MaMonHoc=@maso", connection);
                command.Parameters.Add("@maso", OleDbType.Numeric).Value = maMonHoc;
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
