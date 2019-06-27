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
    public class NganhHocDAL : MSAccessDatabase
    {
        public NganhHocDAL(OleDbConnection connection) : base(connection) { }
        public NganhHocDAL(string connectionString) : base(connectionString) { }

        public List<NganhHoc> ReadAllItems()
        {
            List<NganhHoc> nganhHocs = new List<NganhHoc>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM NGANH_HOC ORDER BY TenNganhHoc ASC", connection);
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
                NganhHoc nganhHoc = new NganhHoc();
                nganhHoc.MaNganhHoc = int.Parse(row["MaNganhHoc"].ToString());
                nganhHoc.Khoa = int.Parse(row["Khoa"].ToString());
                nganhHoc.TenNganhHoc = row["TenNganhHoc"].ToString();
                nganhHocs.Add(nganhHoc);
            }
            return nganhHocs;
        }
    }
}
