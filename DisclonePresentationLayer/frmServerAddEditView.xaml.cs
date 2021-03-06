using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class frmServerAddEditView : Window
    {
        private ServerVM _server;
        private IServerManager _serverManager;
        private User _user;


        private bool addServer = false;
        private bool editServer = false;
        private bool imageUpdated = false;


        public frmServerAddEditView(User user,IServerManager serverManager)
        {
            addServer = true;
            _user = user;
            _serverManager = serverManager;
            _server = new ServerVM();
            InitializeComponent();
        }
        public frmServerAddEditView(ServerVM server, User user, IServerManager serverManager)
        {
            _server = server;
            _serverManager = serverManager;
            _user = user;
            if(server.OwnerUserID == user.UserID)
            {
                editServer = true;
            }
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (addServer)
            {
                //new server code
                tbServerOwner.Text = _user.UserName;
            }
            else if (editServer)
            {
                //editing server
                DisplayServer();               
            }
            else
            {
                //view server
                DisplayServer();
                tbServerName.IsReadOnly = true;
                tbServerTag.IsReadOnly = true;
                btnOpenImageFile.Visibility = Visibility.Hidden;
                btnSaveServer.Visibility = Visibility.Hidden;
                
            }
        }

        private void DisplayServer()
        {
            if(_server.ServerImageSource != null)
            {
                ibServerImage.ImageSource = _server.ServerImageSource;
            }            
            tbServerName.Text = _server.Name;
            tbServerOwner.Text = _server.OwnerUserName;
            tbServerTag.Text = _server.Tag;
        }

        private void btnOpenImageFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (true == dlg.ShowDialog())
            {               
                string fileName = dlg.FileName;

                //ibServerImage.ImageSource = ImageHelper.ConvertFileToImageSource(_fileName);
                _server.ServerImage = ImageHelper.ConvertFileToByteArray(fileName);
                _server.ServerImageSource = ImageHelper.ConvertByteArrayToImageSource(_server.ServerImage);
                ibServerImage.ImageSource = _server.ServerImageSource;
                imageUpdated = true;
            }
        }

        private void btnSaveServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validateInputs())
                {
                    if (addServer)
                    {
                        //new server code
                        _server.Name = tbServerName.Text;
                        _server.Tag = tbServerTag.Text;
                        _server.OwnerUserID = _user.UserID;
                        if (_serverManager.AddServer(_server, imageUpdated))
                        {
                            this.DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed, but no error to show.");
                        }
                    }
                    else if (editServer)
                    {
                        _server.Name = tbServerName.Text;
                        _server.Tag = tbServerTag.Text;
                        if (_serverManager.EditServer(_server, _user.UserID, imageUpdated))
                        {
                            this.DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Update failed, but no error to show.");
                        }
                    }
                    else
                    {
                        //view server
                        MessageBox.Show("How did you even click the button?");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }

        private bool validateInputs()
        {
            bool result = false;

            if (!tbServerName.Text.isValidServerName())
            {
                MessageBox.Show("Invalid server name: name must be between 1-100 in length");
            } else if (!tbServerTag.Text.isTagValid())
            {
                MessageBox.Show("Invalid tag name: tag must be 8 characters long");
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
