﻿#pragma checksum "C:\utilitypoleinspection-mustbechanged_v1 (1)_temp\utilitypoleinspection-mustbechanged_v1\AddPoleInfoTasks.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E6DCDBDEF7D36FACAC203674A221C653"
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
    partial class AddPoleInfoTasks : 
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
                    this.btnLoadTasks = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 27 "..\..\..\AddPoleInfoTasks.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.btnLoadTasks).Click += this.btnLoadTasks_Click;
                    #line default
                }
                break;
            case 2:
                {
                    this.goBack = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 28 "..\..\..\AddPoleInfoTasks.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.goBack).Click += this.goBack_Click;
                    #line default
                }
                break;
            case 3:
                {
                    this.dtPickerLoadTask = (global::Windows.UI.Xaml.Controls.DatePicker)(target);
                }
                break;
            case 4:
                {
                    this.lvTasks = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 31 "..\..\..\AddPoleInfoTasks.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.lvTasks).ItemClick += this.lvTasks_ItemClick;
                    #line default
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

