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
    public class KhoaDAL : MSAccessDatabase
    {
        public KhoaDAL(OleDbConnection connection) : base(connection) { }
        public KhoaDAL(string connectionString) :base(connectionString) { }

        public List<Khoa> ReadAllItems()
        {
            List<Khoa> khoas = new List<Khoa>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM KHOA ORDER BY TenKhoa ASC", connection);
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
                Khoa khoa = new Khoa();
                khoa.MaKhoa = int.Parse(row["MaKhoa"].ToString());
                khoa.TenKhoa = row["TenKhoa"].ToString();
                khoas.Add(khoa);
            }
            return khoas;
        }
    }
}
