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
    public class MonHocDAL : MSAccessDatabase
    {
        public MonHocDAL(OleDbConnection connection) : base(connection) { }
        public MonHocDAL(string connectionString) : base(connectionString) { }
    }
}
