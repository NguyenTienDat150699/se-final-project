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
    public class DsMonHocMoDAL : MSAccessDatabase
    {
        public DsMonHocMoDAL(OleDbConnection connection) : base(connection) { }
        public DsMonHocMoDAL(string connectionString): base(connectionString) { }

        public DsMonHocMo ReadItemByHocKyAndNamHoc(DsMonHocMo dsMonHocMo)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM DS_MON_HOC_MO WHERE HocKy=@hocky AND NamHoc=@namhoc", 
                    connection);
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = dsMonHocMo.HocKy;
                command.Parameters.Add("@namhoc", OleDbType.Numeric).Value = dsMonHocMo.NamHoc;
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
            DsMonHocMo dsMHM = new DsMonHocMo();
            dsMHM.MaDsMonHocMo = int.Parse(dataTable.Rows[0]["MaDsMonHocMo"].ToString());
            dsMHM.HocKy = int.Parse(dataTable.Rows[0]["HocKy"].ToString());
            dsMHM.NamHoc = int.Parse(dataTable.Rows[0]["NamHoc"].ToString());
            return dsMHM;
        }

        public bool IsExistedByHocKyAndNamHoc(DsMonHocMo dsMonHocMo)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT COUNT(MaDsMonHocMo) FROM DS_MON_HOC_MO " +
                    "WHERE HocKy=@hocky AND NamHoc=@namhoc", connection);
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = dsMonHocMo.HocKy;
                command.Parameters.Add("@namhoc", OleDbType.Numeric).Value = dsMonHocMo.NamHoc;
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

        public void CreateItemByHocKyAndNamHoc(DsMonHocMo dsMonHocMo)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO DS_MON_HOC_MO (HocKy, NamHoc) VALUES (@hocky, @namhoc)",
                    connection);
                command.Parameters.Add("@hocky", OleDbType.Numeric).Value = dsMonHocMo.HocKy;
                command.Parameters.Add("@namhoc", OleDbType.Numeric).Value = dsMonHocMo.NamHoc;

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
    }
}
