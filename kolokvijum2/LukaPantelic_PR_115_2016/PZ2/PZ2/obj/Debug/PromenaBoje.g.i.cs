﻿#pragma checksum "..\..\PromenaBoje.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A793FEC83BEB8094917C35473AF45A1BED74AAE4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PZ2;
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


namespace PZ2 {
    
    
    /// <summary>
    /// PromenaBoje
    /// </summary>
    public partial class PromenaBoje : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\PromenaBoje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox fillComboBox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\PromenaBoje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem orange;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\PromenaBoje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem azure;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\PromenaBoje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem black;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\PromenaBoje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem purple;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\PromenaBoje.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button menjanjeBojeBTN;
        
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
            System.Uri resourceLocater = new System.Uri("/PZ2;component/promenaboje.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PromenaBoje.xaml"
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
            this.fillComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.orange = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 3:
            this.azure = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 4:
            this.black = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 5:
            this.purple = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 6:
            this.menjanjeBojeBTN = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\PromenaBoje.xaml"
            this.menjanjeBojeBTN.Click += new System.Windows.RoutedEventHandler(this.PromenaBojeBTN_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

