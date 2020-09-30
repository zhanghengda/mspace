﻿using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mmc.Mspace.Services.NetRouteAnalysisService
{
    [GeneratedCode("System.Xml", "4.0.30319.18408")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://www.esri.com/schemas/ArcGIS/10.1")]
    [Serializable]
    public class NAServerNetworkDirections : INotifyPropertyChanged
    {
        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 0)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] AvailableStyleNames
        {
            get
            {
                return this.availableStyleNamesField;
            }
            set
            {
                this.availableStyleNamesField = value;
                this.RaisePropertyChanged("AvailableStyleNames");
            }
        }

        [XmlArray(Form = XmlSchemaForm.Unqualified, Order = 1)]
        [XmlArrayItem("String", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public string[] SupportedLanguages
        {
            get
            {
                return this.supportedLanguagesField;
            }
            set
            {
                this.supportedLanguagesField = value;
                this.RaisePropertyChanged("SupportedLanguages");
            }
        }

        // (add) Token: 0x06000A79 RID: 2681 RVA: 0x00014E80 File Offset: 0x00013080
        // (remove) Token: 0x06000A7A RID: 2682 RVA: 0x00014EB8 File Offset: 0x000130B8
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            bool flag = propertyChanged != null;
            if (flag)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string[] availableStyleNamesField;

        private string[] supportedLanguagesField;
    }
}