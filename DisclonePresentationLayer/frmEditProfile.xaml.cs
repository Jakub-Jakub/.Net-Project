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
    /// Interaction logic for frmEditProfile.xaml
    /// </summary>
    public partial class frmEditProfile : Window
    {
        private UserVM _user;
        private IUserManager _userManager;

        private bool imageUpdated = false;
        public frmEditProfile(UserVM user, IUserManager userManager)
        {
            InitializeComponent();
            _user = user;
            _userManager = userManager;
            if (_user.UserImageSource != null)
            {
                ibUserImage.ImageSource = _user.UserImageSource;
            }
        }

        private void btnOpenImageFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (true == dlg.ShowDialog())
            {
                string fileName = dlg.FileName;

                //ibServerImage.ImageSource = ImageHelper.ConvertFileToImageSource(_fileName);
                _user.UserImage = ImageHelper.ConvertFileToByteArray(fileName);
                _user.UserImageSource = ImageHelper.ConvertByteArrayToImageSource(_user.UserImage);
                ibUserImage.ImageSource = _user.UserImageSource;
                imageUpdated = true;
            }
        }

        private void btnSaveUserEdits_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    if (pwdNewPassword.Password.Length > 0)
                    {
                        //change password
                        if(pwdNewPassword.Password == pwdNewPasswordConfirm.Password)
                        {
                            if(_userManager.UpdatePassword(_user.Email, pwdCurrentPassword.Password, pwdNewPassword.Password))
                        {
                            MessageBox.Show("Password Updated.");
                        }
                        }
                        else
                        {
                            throw new ApplicationException("Password do not match");
                        }
                    }
                    if (imageUpdated)
                    {
                        if(_userManager.UpdateUserImage(_user.UserID, _user.UserImage))
                    {
                        MessageBox.Show("UserImage Updated");
                    }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }

            BadPractice.User = _user;
            this.DialogResult = true;
        }
    }
}
