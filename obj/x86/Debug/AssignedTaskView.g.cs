﻿#pragma checksum "C:\utilitypoleinspection-mustbechanged_v1 (1)_temp\utilitypoleinspection-mustbechanged_v1\AssignedTaskView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA54D05D39F8ABF14A9B16DC1F508273"
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
    partial class AssignedTaskView : 
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
                    this.drpPoleID = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 28 "..\..\..\AssignedTaskView.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.drpPoleID).SelectionChanged += this.drpPoleID_SelectionChanged;
                    #line default
                }
                break;
            case 2:
                {
                    this.txbLoginID = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.vieW = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 30 "..\..\..\AssignedTaskView.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.vieW).Click += this.View_data;
                    #line default
                }
                break;
            case 4:
                {
                    this.dtPickerLoadTask = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 5:
                {
                    this.txbdateTaskAdded = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.dtPickerPerformTask = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 7:
                {
                    this.txbdateTaskPerformed = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.txtPoleHeight = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9:
                {
                    this.txbPoleHeight = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10:
                {
                    this.txbTransformerLoad = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 11:
                {
                    this.txtTransformerLoad = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12:
                {
                    this.goBack = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 62 "..\..\..\AssignedTaskView.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.goBack).Click += this.goBack_Click;
                    #line default
                }
                break;
            case 13:
                {
                    this.textBoxNotes = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14:
                {
                    this.textNotes = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 15:
                {
                    this.txtTakePhoto = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 16:
                {
                    this.imagePreivew = (global::Windows.UI.Xaml.Controls.Image)(target);
                }
                break;
            case 17:
                {
                    this.txtBlkPhotos = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 18:
                {
                    this.cbEnvironmental = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 19:
                {
                    this.cbVegetation = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 20:
                {
                    this.cbNonStandard = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 21:
                {
                    this.cbROW = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 22:
                {
                    this.cbSimple = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 23:
                {
                    this.cbAdditionalPole = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 24:
                {
                    this.cbReturn = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
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

