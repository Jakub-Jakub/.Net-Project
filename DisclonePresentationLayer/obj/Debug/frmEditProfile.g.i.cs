﻿#pragma checksum "..\..\frmEditProfile.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AD4A2BC9E7476EE2B90D68A333B7151DE27997D5498C97609B5A078BF5E6D345"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DisclonePresentationLayer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DisclonePresentationLayer {
    
    
    /// <summary>
    /// frmEditProfile
    /// </summary>
    public partial class frmEditProfile : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\frmEditProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush ibUserImage;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\frmEditProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOpenImageFile;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\frmEditProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwdCurrentPassword;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\frmEditProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwdNewPassword;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\frmEditProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwdNewPasswordConfirm;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\frmEditProfile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveUserEdits;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DisclonePresentationLayer;component/frmeditprofile.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmEditProfile.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ibUserImage = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 2:
            this.btnOpenImageFile = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\frmEditProfile.xaml"
            this.btnOpenImageFile.Click += new System.Windows.RoutedEventHandler(this.btnOpenImageFile_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pwdCurrentPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.pwdNewPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.pwdNewPasswordConfirm = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.btnSaveUserEdits = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\frmEditProfile.xaml"
            this.btnSaveUserEdits.Click += new System.Windows.RoutedEventHandler(this.btnSaveUserEdits_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
