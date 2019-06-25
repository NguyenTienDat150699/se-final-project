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
    }
}
