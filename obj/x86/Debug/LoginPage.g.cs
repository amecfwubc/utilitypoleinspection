﻿#pragma checksum "C:\Users\salva\Documents\GitHub\utilitypoleinspection-mustbechanged_18-10-2015\LoginPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A1601486D6EAD5DB3F21ADB7309DE92C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmecFWUPI
{
    partial class LoginPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.progressRing1 = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 2:
                {
                    this.txtLoginID = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    #line 40 "..\..\..\LoginPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.TextBox)this.txtLoginID).TextChanged += this.txtLoginID_TextChanged;
                    #line default
                }
                break;
            case 3:
                {
                    this.txtLoginPass = (global::Windows.UI.Xaml.Controls.PasswordBox)(target);
                }
                break;
            case 4:
                {
                    this.btnLogin = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 42 "..\..\..\LoginPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnLogin).Click += this.btnLogin_Click;
                    #line default
                }
                break;
            case 5:
                {
                    this.txbLoginID = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.txbLoginPass = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7:
                {
                    this.txtError = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

