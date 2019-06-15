using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JENCORECORP
{
    public partial class USERS
    {
        private long _userID;
        public long UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private String _userName;
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private String _loginID;
        public String LoginID
        {
            get { return _loginID; }
            set { _loginID = value; }
        }

        private String _password;
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private String _roleType;
        public String RoleType
        {
            get { return _roleType; }
            set { _roleType = value; }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }


    }
}
