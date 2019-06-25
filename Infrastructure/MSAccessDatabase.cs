using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class MSAccessDatabase
    {
        protected OleDbConnection connection;

        public MSAccessDatabase(OleDbConnection dbConnection)
        {
            connection = dbConnection;
        }
        public MSAccessDatabase(string connectionString)
        {
            connection = new OleDbConnection(connectionString);
        }
    }
}
