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
    public class QuanHuyenDAL : MSAccessDatabase
    {
        public QuanHuyenDAL(OleDbConnection connection) : base(connection) { }
        public QuanHuyenDAL(string connectionString) : base(connectionString) { }
    }
}
