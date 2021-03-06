﻿#pragma checksum "..\..\..\UavTracing\UavControlView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4A371CEE6BFE7CB3A2C40EF5B132BA3AE682434F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GFramework.BlankWindow;
using LiveCharts.Wpf;
using Mmc.Mspace.Theme.Controls;
using Mmc.Mspace.Theme.Helper;
using Mmc.Mspace.UavModule.Converter;
using Mmc.Mspace.UavModule.UavTracing;
using Mmc.Wpf.Toolkit.Attached;
using Mmc.Wpf.Toolkit.Controls;
using Mmc.Wpf.Toolkit.Controls.Panels;
using Mmc.Wpf.Toolkit.Controls.Progress;
using Mmc.Wpf.Toolkit.Converters;
using Mmc.Wpf.Toolkit.MarkupExtensions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Mmc.Mspace.UavModule.UavTracing {
    
    
    /// <summary>
    /// UavControlView
    /// </summary>
    public partial class UavControlView : GFramework.BlankWindow.BlankWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 101 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox routelistBox;
        
        #line default
        #line hidden
        
        
        #line 239 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BatteryNormal;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BatteryWarning;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Mmc.Mspace.Theme.Controls.BatteryView BatteryView;
        
        #line default
        #line hidden
        
        
        #line 276 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run CurrentPowerText;
        
        #line default
        #line hidden
        
        
        #line 284 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run WarningBatteryText;
        
        #line default
        #line hidden
        
        
        #line 604 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioCamLock;
        
        #line default
        #line hidden
        
        
        #line 605 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioCamControl;
        
        #line default
        #line hidden
        
        
        #line 606 "..\..\..\UavTracing\UavControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton RadioCamReset;
        
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
            System.Uri resourceLocater = new System.Uri("/Mmc.Mspace.UavModule;component/uavtracing/uavcontrolview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UavTracing\UavControlView.xaml"
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
            this.routelistBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.BatteryNormal = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.BatteryWarning = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.BatteryView = ((Mmc.Mspace.Theme.Controls.BatteryView)(target));
            return;
            case 5:
            this.CurrentPowerText = ((System.Windows.Documents.Run)(target));
            return;
            case 6:
            this.WarningBatteryText = ((System.Windows.Documents.Run)(target));
            return;
            case 7:
            this.RadioCamLock = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.RadioCamControl = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 9:
            this.RadioCamReset = ((System.Windows.Controls.RadioButton)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

