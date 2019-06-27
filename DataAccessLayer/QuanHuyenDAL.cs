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
    public class QuanHuyenDAL : MSAccessDatabase
    {
        public QuanHuyenDAL(OleDbConnection connection) : base(connection) { }
        public QuanHuyenDAL(string connectionString) : base(connectionString) { }

        public List<QuanHuyen> ReadItemsByTinhThanh(int tinhThanh)
        {
            List<QuanHuyen> quanHuyens = new List<QuanHuyen>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM QUAN_HUYEN WHERE TinhThanh=@matinhthanh " +
                    "ORDER BY TenQuanHuyen ASC", connection);
                command.Parameters.Add("@matinhthanh", OleDbType.Numeric).Value = tinhThanh;
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
                QuanHuyen quanHuyen = new QuanHuyen();
                quanHuyen.MaQuanHuyen = int.Parse(row["MaQuanHuyen"].ToString());
                quanHuyen.TenQuanHuyen = row["TenQuanHuyen"].ToString();
                quanHuyen.TinhThanh = int.Parse(row["TinhThanh"].ToString());
                quanHuyen.ThongTin = row["ThongTin"].ToString();
                quanHuyens.Add(quanHuyen);
            }
            return quanHuyens;
        }
    }
}
