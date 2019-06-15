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
    /// Interaction logic for AddUsers.xaml
    /// </summary>
    public partial class AddUsers : UserControlClass
    {
        USERS User;
        public AddUsers()
        {
            InitializeComponent();
        }

        public AddUsers(USERS User)
        {            
            InitializeComponent();
            this.User = User;
            LoadSelectedUser(this.User);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ((ChildWindow)(this.Parent)).Close();
        }

        private void LoadSelectedUser(USERS SelectedUser)
        {
            TxtUserName.Text = SelectedUser.UserName;
            TxtUserLoginId.Text = SelectedUser.LoginID;
            if (SelectedUser.IsActive == true)
                UserActive.IsChecked = true;
            else
                UserInActive.IsChecked = true;
            cbRoleType.SelectedValue = SelectedUser.RoleType;
        }
    }
}
