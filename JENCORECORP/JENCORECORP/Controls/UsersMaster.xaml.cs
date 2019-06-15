using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for UsersMaster.xaml
    /// </summary>
    public partial class UsersMaster : UserControlClass
    {
        UserDAL userDal;
        USERS SelectedUser;
        public UsersMaster()
        {
            InitializeComponent();
            Loaded += UsersMaster_Loaded;
        }

        void UsersMaster_Loaded(object sender, RoutedEventArgs e)
        {
            userDal = new UserDAL(Library.JENDBManager);
            LoadAllUser();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _UserschildWindow.Content = new AddUsers(SelectedUser);
            _UserschildWindow.Show();
        }

        private void LoadAllUser()
        {
            List<USERS> Users = userDal.GetAllJenUsers();
            USERS select = new USERS() { UserName = "Select"};
            Users.Insert(0, select);
            cbUsers.ItemsSource = Users;
            cbUsers.SelectedIndex = 0;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ((ChildWindow)(this.Parent)).Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            _UserschildWindow.Content = new AddUsers();
            _UserschildWindow.Show();
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedUser = userDal.GetAllJenUsers().Find(u => u.UserID == Convert.ToInt64(cbUsers.SelectedValue));
            if (SelectedUser!=null)
            LoadSelectedUser(SelectedUser);
        }

        private void LoadSelectedUser(USERS SelectedUser)
        {
            LblUserName.Text = SelectedUser.UserName;
            LblUserLoginId.Text = SelectedUser.LoginID;
            LblUserIsActive.Text = SelectedUser.IsActive == true?"ACTIVE" : "IN ACTIVE";
            LblUserRoleType.Text = SelectedUser.RoleType;
        }
    }
}
