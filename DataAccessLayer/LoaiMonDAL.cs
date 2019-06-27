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
    public class LoaiMonDAL : MSAccessDatabase
    {
        public LoaiMonDAL(OleDbConnection connection) : base (connection) { }
        public LoaiMonDAL(string connectionString) :base(connectionString) { }

        public List<LoaiMon> ReadAllItems()
        {
            List<LoaiMon> loaiMons = new List<LoaiMon>();
            DataTable dataTable = new DataTable();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                OleDbCommand command = new OleDbCommand(
                    "SELECT * FROM LOAI_MON ORDER BY TenLoaiMon ASC", connection);
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
                LoaiMon loaiMon = new LoaiMon();
                loaiMon.MaLoaiMon = int.Parse(row["MaLoaiMon"].ToString());
                loaiMon.TenLoaiMon = row["TenLoaiMon"].ToString();
                loaiMon.SoTietCho1TinChi = byte.Parse(row["SoTietCho1TinChi"].ToString());
                loaiMon.SoTienCho1TinChi = double.Parse(row["SoTienCho1TinChi"].ToString());
                loaiMons.Add(loaiMon);
            }
            return loaiMons;
        }
    }
}
