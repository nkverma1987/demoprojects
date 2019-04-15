using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCDemoNew.App_Start
{
    public class DatabaseHelper
    {
        private static string connectionString;
        private static SqlParameter errorCode;
        private static SqlParameter errorMessage;

        static DatabaseHelper()
        {
            connectionString = ConfigurationManager.ConnectionStrings["test"].ConnectionString;
            //errorCode = new SqlParameter() { ParameterName = StoredProcedureParameter.ErrorCode, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 50 };
            //errorMessage = new SqlParameter() { ParameterName = StoredProcedureParameter.ErrorMessage, SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 200 };
        }

        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        public static SqlParameter ErrorCodeParameter
        {
            get
            {
                return errorCode;
            }
        }

        public static SqlParameter ErrorMessageParameter
        {
            get
            {
                return errorMessage;
            }
        }

    }
}