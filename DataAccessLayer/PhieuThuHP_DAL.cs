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
    public class PhieuThuHP_DAL : MSAccessDatabase
    {
        public PhieuThuHP_DAL(OleDbConnection connection) : base(connection) { }
        public PhieuThuHP_DAL(string connectionString) : base(connectionString) { }

        public void CreateItem(PhieuThuHP phieuThuHP)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO PHIEU_THUHP (SoPhieuThuHP, PhieuDKHP, NgayLap, SoTienThu) " +
                    "VALUES (@sophieu, @phieudkhp, @ngaylap, @sotien)",
                    connection);
                command.Parameters.Add("@sophieu", OleDbType.Numeric).Value = phieuThuHP.SoPhieuThuHP;
                command.Parameters.Add("@phieudkhp", OleDbType.Numeric).Value = phieuThuHP.PhieuDKHP;
                command.Parameters.Add("@ngaylap", OleDbType.Date).Value = phieuThuHP.NgayLap;
                command.Parameters.Add("@sotien", OleDbType.Currency).Value = phieuThuHP.SoTienThu;

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

        public bool IsMaSoExisted(int soPhieuThuHP)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT COUNT(SoPhieuDKHP) FROM PHIEU_THUHP WHERE SoPhieuDKHP=@maso", connection);
                command.Parameters.Add("@maso", OleDbType.Numeric).Value = soPhieuThuHP;
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
