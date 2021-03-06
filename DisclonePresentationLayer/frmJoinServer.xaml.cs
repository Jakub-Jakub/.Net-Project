using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DisclonePresentationLayer
{
    /// <summary>
    /// Interaction logic for frmJoinServer.xaml
    /// </summary>
    public partial class frmJoinServer : Window
    {
        private User _user;
        private IUserManager _userManager;
        public frmJoinServer(User user, IUserManager userManager)
        {
            InitializeComponent();

            _user = user;
            _userManager = userManager;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_userManager.AddUserToServerWithTag(_user.UserID, tbTag.Text))
                {
                    this.DialogResult = true;
                }
                else
                {
                    throw new ApplicationException("Was unable to add you to the server, is the tag correct?");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }
    }
}
