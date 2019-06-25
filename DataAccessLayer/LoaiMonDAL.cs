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
    }
}
