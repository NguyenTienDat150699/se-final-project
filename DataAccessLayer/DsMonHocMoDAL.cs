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
    public class DsMonHocMoDAL : MSAccessDatabase
    {
        public DsMonHocMoDAL(OleDbConnection connection) : base(connection) { }
        public DsMonHocMoDAL(string connectionString): base(connectionString) { }
    }
}
