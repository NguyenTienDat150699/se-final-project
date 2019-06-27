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
    public class DoiTuongUuTienDAL : MSAccessDatabase
    {
        public DoiTuongUuTienDAL(OleDbConnection connection) : base(connection) { }
        public DoiTuongUuTienDAL(string connectionString) : base(connectionString) { }

        public List<DoiTuongUuTien> ReadAllItems()
        {
            List<DoiTuongUuTien> doiTuongUuTiens = new List<DoiTuongUuTien>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM DOI_TUONG_UU_TIEN ORDER BY MaDTUT ASC", connection);
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
                DoiTuongUuTien doiTuongUuTien = new DoiTuongUuTien();
                doiTuongUuTien.MaDTUT = int.Parse(row["MaDTUT"].ToString());
                doiTuongUuTien.TenDTUT = row["TenDTUT"].ToString();
                doiTuongUuTien.TiLeMienGiam = double.Parse(row["TiLeMienGiam"].ToString());
                doiTuongUuTiens.Add(doiTuongUuTien);
            }
            return doiTuongUuTiens;
        }
    }
}
