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
    public class UserDAL
    {
        private DataBaseManager DBManager;

        public UserDAL(DataBaseManager DBManager)
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

        public USERS GetUserById(int UserId)
        {
            USERS Result = new USERS();
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM Users WHERE ISACTIVE = 1 AND USERID = " + UserId;
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

        public List<USERS> GetAllJenUsers()
        {
            List<USERS> UserList = new List<USERS>();
            USERS Result;
            DataSet ds = new DataSet();
            string CommandText = "SELECT * FROM Users";
            ds = DBManager.ExecuteDataSet(CommandText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Result = new USERS();
                        Result.UserID = Convert.ToInt64(Convert.IsDBNull(dr["UserID"]) ? 0 : dr["UserID"]);
                        Result.IsActive = Convert.ToBoolean(dr["IsActive"]);
                        Result.LoginID = Convert.ToString(Convert.IsDBNull(dr["LoginID"]) ? "" : dr["LoginID"]);
                        Result.UserName = Convert.ToString(Convert.IsDBNull(dr["Name"]) ? "" : dr["Name"]);
                        Result.RoleType = Convert.ToString(Convert.IsDBNull(dr["RoleType"]) ? "" : dr["RoleType"]);
                        UserList.Add(Result);
                    }
                }
            }
            return UserList;
        }

        public bool SaveUser(USERS USER)
        {
            bool IsSuccess = false;
            int Result = 0;
            string CommandText = "Insert into USERS(Name,LoginID,RoleType,IsActive) " + " values('" + USER.UserName + "','" + USER.LoginID + "','"
                + USER.RoleType + "'," + USER.IsActive + ")";
            Result = DBManager.ExecuteNonQuery(CommandText);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }

        public bool UpdateUser(USERS USER)
        {
            bool IsSuccess = false;
            int Result = 0;
            string CommandText = "update USERS set Name = '" + USER.UserName + "',LoginID = '" + USER.LoginID + "',RoleType = '"
                + USER.RoleType + "',IsActive = " + USER.IsActive + "' where userid = " + USER.UserID;
            Result = DBManager.ExecuteNonQuery(CommandText);
            if (Result > 0)
                IsSuccess = true;
            return IsSuccess;
        }
    }
}
