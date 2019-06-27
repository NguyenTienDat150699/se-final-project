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
                    "SELECT MonHoc, LoaiMon, SoTinChi" +
                    "FROM MO_HOC_MO, MON_HOC " +
                    "WHERE DsMonHocMo=@mads AND MaMonHoc=MonHoc " +
                    "ORDER BY HocKy ASC, MonHoc ASC", connection);
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
    }
}
