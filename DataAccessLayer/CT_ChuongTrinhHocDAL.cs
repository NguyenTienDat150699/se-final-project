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
    public class CT_ChuongTrinhHocDAL : MSAccessDatabase
    {
        public CT_ChuongTrinhHocDAL(OleDbConnection connection) : base(connection) { }
        public CT_ChuongTrinhHocDAL(string connectionString) : base(connectionString) { }
    }
}
