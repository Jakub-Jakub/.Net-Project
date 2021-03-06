using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace DisclonePresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserManager _userManager = new UserManager();
        private IServerManager _serverManger = new ServerManager();
        private IChatroomManager _chatroomManager = new ChatroomManager();
        private IMessageManager _messageManager = new MessageManager();
        private UserVM _user = null;
        private Chatroom _currentChatroom = null;
        private Server _currentServer = null;
        private List<ServerVM> _servers = null;

        public MainWindow()
        {
            InitializeComponent();
            LoginScreen();
            
        }
        private void FillUserInfo()
        {
            lblCurrentUser.Content = _user.UserName;
            if (_user.UserImageSource != null) 
            { 
                ibUserImage.ImageSource = _user.UserImageSource;
            }
        }
        private void FillServerList()
        {
            lstboxServerList.Items.Clear();
            try { 
            _servers = _serverManger.GetUserServerList(_user.UserID);

            if (_user != null && _servers != null)
            {
                foreach(ServerVM server in _servers)
                {                    
                    lstboxServerList.Items.Add(server);
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }
        private void FillChatroomList()
        {
            lstboxChatroomList.Items.Clear();
            if (_currentServer != null)
            {
                //only server owner can add more chatrooms
                if (_currentServer.OwnerUserID == _user.UserID)
                {                  
                    splCreateChatroom.Visibility = Visibility.Visible;
                }
                else
                {
                    splCreateChatroom.Visibility = Visibility.Collapsed;
                }
                try
                {
                    List<Chatroom> chatrooms = _chatroomManager.GetServerChatrooms(_currentServer.ServerID);
                    foreach (var chatroom in chatrooms)
                    {
                        lstboxChatroomList.Items.Add(chatroom);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }
        private void FillMessageList()
        {
            lstboxMessages.Items.Clear();
            if (_currentChatroom != null)
            {
                try
                {
                    List<MessageVM> messageVMs = _messageManager.GetChatroomMessages(_currentChatroom.ChatroomID);
                    foreach (var message in messageVMs)
                    {
                        lstboxMessages.Items.Add(message);
                    }
                    lstboxMessages.Items.MoveCurrentToLast();
                    lstboxMessages.ScrollIntoView(lstboxMessages.Items.CurrentItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }
        private void FillUserList()
        {
            lstboxUserList.Items.Clear();
            if(_currentServer != null)
            {
                try
                {
                    List<UserVM> users = _userManager.GetServerUserList(_currentServer.ServerID);
                    foreach (var user in users)
                    {
                        lstboxUserList.Items.Add(user);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }

        /*
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // logic to see if this is a login or logout
            if ((string)btnLogin.Content == "Login")
            {
                try
                {
                    _user = _userManager.AuthenticateUser(txtEmail.Text, pwdPassword.Password);
                    MessageBox.Show(_user.UserName + " is logged in.");
                                    
                    FillServerList();
                    /*
                    // check for newuser
                    if (pwdPassword.Password == "newuser")
                    {
                        var updatePassword = new frmUpdatePassword(_userManager,
                            _user, true);
                        if (!updatePassword.ShowDialog() == true)
                        {
                            // log the person out
                            _user = null;
                            resetWindow();
                            MessageBox.Show("You must change your password" +
                                "\n on first login to continue.",
                                "Password Change Required", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }
                        // the password was successfully changed.
                    }

                    // update the interface because the user is now logged in
                    btnLogin.IsDefault = false;
                    btnLogin.Content = "Logout";
                    pwdPassword.Password = "";
                    txtEmail.Text = "";
                    txtEmail.Visibility = Visibility.Hidden;
                    lblEmailAddress.Visibility = Visibility.Hidden;
                    pwdPassword.Visibility = Visibility.Hidden;
                    lblPassword.Visibility = Visibility.Hidden;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
            else // logout
            {
                _user = null;
                ResetWindow();
            }
        }
        private void ResetWindow()
        {
            btnLogin.IsDefault = true;
            btnLogin.Content = "Login";
            pwdPassword.Password = "";
            txtEmail.Text = "";
            txtEmail.Visibility = Visibility.Visible;
            lblEmailAddress.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
        }
        private void AutoLogin()
        {
            if ((string)btnLogin.Content == "Login")
            {
                try
                {
                    _user = _userManager.AuthenticateUser("jakub@domain.com", "newuser");
                    MessageBox.Show(_user.UserName + " is logged in.");
                    var serverManger = new ServerManager();
                    FillServerList();
                    /*
                    // check for newuser
                    if (pwdPassword.Password == "newuser")
                    {
                        var updatePassword = new frmUpdatePassword(_userManager,
                            _user, true);
                        if (!updatePassword.ShowDialog() == true)
                        {
                            // log the person out
                            _user = null;
                            resetWindow();
                            MessageBox.Show("You must change your password" +
                                "\n on first login to continue.",
                                "Password Change Required", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }
                        // the password was successfully changed.
                    }

                    // update the interface because the user is now logged in
                    btnLogin.IsDefault = false;
                    btnLogin.Content = "Logout";
                    pwdPassword.Password = "";
                    txtEmail.Text = "";
                    txtEmail.Visibility = Visibility.Hidden;
                    lblEmailAddress.Visibility = Visibility.Hidden;
                    pwdPassword.Visibility = Visibility.Hidden;
                    lblPassword.Visibility = Visibility.Hidden;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
            else // logout
            {
                _user = null;
                ResetWindow();
            }
        } 
*/

        private void lstboxServerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServerVM server = (ServerVM)lstboxServerList.SelectedItem;
            _currentServer = server;
            _currentChatroom = null;
            lstboxChatroomList.Items.Clear();
            lstboxMessages.Items.Clear();
            FillChatroomList();
            FillUserList();
        }

        private void lstboxMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void lstboxChatroomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Chatroom chatroom = (Chatroom)lstboxChatroomList.SelectedItem;
            _currentChatroom = chatroom;
            FillMessageList();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && tbMessage.Text.Length != 0 && _currentChatroom != null)
            {
                try
                {
                    _messageManager.AddChatroomMessage(_currentChatroom.ChatroomID, _user.UserID, tbMessage.Text);
                    tbMessage.Text = "";
                    FillMessageList();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
                }
            }
        }

        private void btnAddServer_Click(object sender, RoutedEventArgs e)
        {
            var joinServerWindow = new frmJoinServer(_user, _userManager);
            if (joinServerWindow.ShowDialog() == true)
            {
                FillServerList();
            }
        }

        private void lstboxServerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (ServerVM)lstboxServerList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("You need to select a server to edit.",
                    "Edit Operation Not Available", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            var serverWindow = new frmServerAddEditView(selectedItem, _user, _serverManger);
            if(serverWindow.ShowDialog() == true)
            {
                FillServerList();
            }

        }

        private void btnCreateServer_Click(object sender, RoutedEventArgs e)
        {
            var serverWindow = new frmServerAddEditView(_user, _serverManger);
            if (serverWindow.ShowDialog() == true)
            {
                FillServerList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var profileEditorWidnow = new frmEditProfile(_user, _userManager);
            if(profileEditorWidnow.ShowDialog() == true)
            {
                _user = BadPractice.User;
                FillUserInfo();
                FillUserList();
            }
        }

        private void btnCreateChatroom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_chatroomManager.AddChatroom(_currentServer.ServerID, tbCreateChatroom.Text))
                {
                    FillChatroomList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            ResetWindow();
            LoginScreen();
        }
        private void LoginScreen()
        {
            var LoginWindow = new LoginWindow(_userManager);
            if (LoginWindow.ShowDialog() == true)
            {
                _user = BadPractice.User;
                FillServerList();
                FillUserInfo();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }
        private void ResetWindow()
        {
            lstboxMessages.Items.Clear();
            lstboxChatroomList.Items.Clear();
            lstboxServerList.Items.Clear();
            lstboxUserList.Items.Clear();
            lblCurrentUser.Content = "";
            ibUserImage.ImageSource = null;
            _user = null;
            _currentChatroom = null;
            _currentServer = null;
            _servers = null;
        }
    }
}
