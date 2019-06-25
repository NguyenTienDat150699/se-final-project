using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PhieuDKHP_DAL : MSAccessDatabase
    {
        public PhieuDKHP_DAL(OleDbConnection connection) : base(connection) { }
        public PhieuDKHP_DAL(string connectionString) : base(connectionString) { }
    }
}
