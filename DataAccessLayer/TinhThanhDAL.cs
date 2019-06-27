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
    public class TinhThanhDAL : MSAccessDatabase
    {
        public TinhThanhDAL(OleDbConnection connection) : base(connection) { }
        public TinhThanhDAL(string connectionString) : base(connectionString) { }

        public List<TinhThanh> ReadAllItems()
        {
            List<TinhThanh> tinhThanhs = new List<TinhThanh>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM TINH_THANH ORDER BY TenTinhThanh ASC", connection);
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
                TinhThanh tinhThanh = new TinhThanh();
                tinhThanh.MaTinhThanh = int.Parse(row["MaTinhThanh"].ToString());
                tinhThanh.TenTinhThanh = row["TenTinhThanh"].ToString();
                tinhThanhs.Add(tinhThanh);
            }
            return tinhThanhs;
        }
    }
}
