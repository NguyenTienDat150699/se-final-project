﻿using Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BaseViewModel : PropertyChangedBaseClass
    {
        protected OleDbConnection dbConnection;
        public BaseViewModel()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            dbConnection = new OleDbConnection(connectionString);
        }
    }
}
