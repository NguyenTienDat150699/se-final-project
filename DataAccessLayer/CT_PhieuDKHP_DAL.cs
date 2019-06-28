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
    public class CT_PhieuDKHP_DAL : MSAccessDatabase
    {
        public CT_PhieuDKHP_DAL(OleDbConnection connection) : base(connection) { }
        public CT_PhieuDKHP_DAL(string connectionString) : base(connectionString) { }

        public void CreateItem(CT_PhieuDKHP cT_PhieuDKHP)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "INSERT INTO CT_PHIEU_DKHP (MonHoc, SoPhieu) VALUES (@monhoc, @sophieu)",
                    connection);
                command.Parameters.Add("@monhoc", OleDbType.Numeric).Value = cT_PhieuDKHP.MonHoc;
                command.Parameters.Add("@sophieu", OleDbType.Numeric).Value = cT_PhieuDKHP.SoPhieu;

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
