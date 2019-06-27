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
    public class HocKyDAL : MSAccessDatabase
    {
        public HocKyDAL(OleDbConnection connection) : base(connection) { }
        public HocKyDAL(string connectionString) : base(connectionString) { }

        public List<HocKy> ReadAllItems()
        {
            List<HocKy> listHocKy = new List<HocKy>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM HOC_KY ORDER BY MaHocKy ASC", connection);
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
                HocKy hocKy = new HocKy();
                hocKy.MaHocKy = int.Parse(row["MaHocKy"].ToString());
                hocKy.TenHocKy = row["TenHocKy"].ToString();
                listHocKy.Add(hocKy);
            }
            return listHocKy;
        }
    }
}
