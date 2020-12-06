using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Yulius.Data.Library;

namespace Yulius.Data.Data
{
    public abstract class BaseRepoMSSQL
    {
        protected readonly string _connStr;
        protected readonly string _secretKey;

        protected readonly IDbConnection _dbConn;
        protected readonly Utilities _utilities;


        public BaseRepoMSSQL(string connStr, string secretKey)
        {
            _connStr = connStr;
            _secretKey = secretKey;

            _dbConn = new SqlConnection(_connStr);
            _utilities = new Utilities();
        }

    }
}
