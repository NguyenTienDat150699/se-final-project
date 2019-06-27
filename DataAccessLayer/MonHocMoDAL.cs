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
    public class MonHocMoDAL : MSAccessDatabase
    {
        public MonHocMoDAL(OleDbConnection connection) : base(connection) { }
        public MonHocMoDAL(string connectionString) : base(connectionString) { }
    }
}
