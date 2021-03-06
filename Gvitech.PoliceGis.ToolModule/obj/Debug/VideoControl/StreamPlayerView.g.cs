﻿#pragma checksum "..\..\..\VideoControl\StreamPlayerView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0627CFAC06C54954C9A5AE9E86814B52235DDAB6"
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
using WebEye.Controls.Wpf.StreamPlayerControl;


namespace Mmc.Mspace.ToolModule.VideoControl {
    
    
    /// <summary>
    /// StreamPlayerView
    /// </summary>
    public partial class StreamPlayerView : GFramework.BlankWindow.BlankWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TitleName;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WebEye.Controls.Wpf.StreamPlayerControl.StreamPlayerControl _streamPlayerControl;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _urlTextBox;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _statusLabel;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _playButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _stopButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\VideoControl\StreamPlayerView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _imageButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Mmc.Mspace.ToolModule;component/videocontrol/streamplayerview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\VideoControl\StreamPlayerView.xaml"
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
            
            #line 9 "..\..\..\VideoControl\StreamPlayerView.xaml"
            ((Mmc.Mspace.ToolModule.VideoControl.StreamPlayerView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.windowLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TitleName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this._streamPlayerControl = ((WebEye.Controls.Wpf.StreamPlayerControl.StreamPlayerControl)(target));
            
            #line 25 "..\..\..\VideoControl\StreamPlayerView.xaml"
            this._streamPlayerControl.StreamFailed += new WebEye.Controls.Wpf.StreamPlayerControl.StreamPlayerControl.StreamFailedEventHandler(this.HandlePlayerEvent);
            
            #line default
            #line hidden
            
            #line 26 "..\..\..\VideoControl\StreamPlayerView.xaml"
            this._streamPlayerControl.StreamStarted += new System.Windows.RoutedEventHandler(this.HandlePlayerEvent);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\VideoControl\StreamPlayerView.xaml"
            this._streamPlayerControl.StreamStopped += new System.Windows.RoutedEventHandler(this.HandlePlayerEvent);
            
            #line default
            #line hidden
            return;
            case 5:
            this._urlTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this._statusLabel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this._playButton = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\VideoControl\StreamPlayerView.xaml"
            this._playButton.Click += new System.Windows.RoutedEventHandler(this.HandlePlayButtonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this._stopButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\VideoControl\StreamPlayerView.xaml"
            this._stopButton.Click += new System.Windows.RoutedEventHandler(this.HandleStopButtonClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this._imageButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\VideoControl\StreamPlayerView.xaml"
            this._imageButton.Click += new System.Windows.RoutedEventHandler(this.HandleImageButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

