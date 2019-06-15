using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RDCore;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace JENCORECORP
{
    public class DAL
    {
        private DataBaseManager DBManager;
        private String DBNAME;

        public DAL(DataBaseManager DBManager)
        {
            this.DBManager = DBManager;
        }


        public USERS CheckJenUsers(String LoginId, String PassWord)
        {
            USERS Result = new USERS();
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM Users WHERE ISACTIVE = 1 AND LOGINID = '" + LoginId + "' AND PASSWORD = '" + PassWord + "'";
            ds = DBManager.ExecuteDataSet(CommandText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Result.UserID = Convert.ToInt64(Convert.IsDBNull(dr["UserID"]) ? 0 : dr["UserID"]);
                        Result.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        Result.LoginID = Convert.ToString(Convert.IsDBNull(dr["LoginID"]) ? "" : dr["LoginID"]);
                        Result.UserName = Convert.ToString(Convert.IsDBNull(dr["Name"]) ? "" : dr["Name"]);
                        Result.RoleType = Convert.ToString(Convert.IsDBNull(dr["RoleType"]) ? "" : dr["RoleType"]);
                    }
                }
            }
            return Result;
        }

    }
}
