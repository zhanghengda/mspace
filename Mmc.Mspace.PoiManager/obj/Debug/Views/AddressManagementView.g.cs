﻿#pragma checksum "..\..\..\Views\AddressManagementView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "32F05C9430ED35C39119D68E074516CF357C1D37"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Mmc.Mspace.PoiManagerModule.Views;
using Mmc.Mspace.Theme.Controls;
using Mmc.Mspace.Theme.Converter;
using Mmc.Mspace.Theme.Helper;
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
using System.Windows.Interactivity;
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


namespace Mmc.Mspace.PoiManagerModule.Views {
    
    
    /// <summary>
    /// AddressManagementView
    /// </summary>
    public partial class AddressManagementView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Views\AddressManagementView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView departmentTree;
        
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
            System.Uri resourceLocater = new System.Uri("/Mmc.Mspace.PoiManagerModule;component/views/addressmanagementview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AddressManagementView.xaml"
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
            this.departmentTree = ((System.Windows.Controls.TreeView)(target));
            
            #line 28 "..\..\..\Views\AddressManagementView.xaml"
            this.departmentTree.AddHandler(System.Windows.Controls.TreeViewItem.SelectedEvent, new System.Windows.RoutedEventHandler(this.DepartmentTree_Selected));
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\Views\AddressManagementView.xaml"
            this.departmentTree.DragOver += new System.Windows.DragEventHandler(this.TreeViewItem_DragOver);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\Views\AddressManagementView.xaml"
            this.departmentTree.Drop += new System.Windows.DragEventHandler(this.TreeViewItem_Drop);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\Views\AddressManagementView.xaml"
            this.departmentTree.MouseMove += new System.Windows.Input.MouseEventHandler(this.TreeViewItem_MouseMove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

