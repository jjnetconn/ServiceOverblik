﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceOverblik.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("User Id=jj;password=buDs7Cs!;server=solcellespecialisten.cnwfj8va3xp9.eu-west-1.r" +
            "ds.amazonaws.com;database=servicebase")]
        public string servicebaseConnectionString {
            get {
                return ((string)(this["servicebaseConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100200")]
        public string InverterProdGrp {
            get {
                return ((string)(this["InverterProdGrp"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public int excelStartRow {
            get {
                return ((int)(this["excelStartRow"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("jj@netconn.dk")]
        public string fakturaEmail {
            get {
                return ((string)(this["fakturaEmail"]));
            }
            set {
                this["fakturaEmail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("support@solcellespecialisten.dk")]
        public string fraEmail {
            get {
                return ((string)(this["fraEmail"]));
            }
            set {
                this["fraEmail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool isMailTest {
            get {
                return ((bool)(this["isMailTest"]));
            }
            set {
                this["isMailTest"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mail.netconn.dk ")]
        public string smptServer {
            get {
                return ((string)(this["smptServer"]));
            }
            set {
                this["smptServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("26")]
        public int smtpPort {
            get {
                return ((int)(this["smtpPort"]));
            }
            set {
                this["smtpPort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("jj@netconn.dk")]
        public string smptUser {
            get {
                return ((string)(this["smptUser"]));
            }
            set {
                this["smptUser"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("buDs76Cs!")]
        public string smtpPass {
            get {
                return ((string)(this["smtpPass"]));
            }
            set {
                this["smtpPass"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool smtpSSL {
            get {
                return ((bool)(this["smtpSSL"]));
            }
            set {
                this["smtpSSL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1.25")]
        public double salesTax {
            get {
                return ((double)(this["salesTax"]));
            }
            set {
                this["salesTax"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("support@solcellespecialisten.dk")]
        public string supportEmail {
            get {
                return ((string)(this["supportEmail"]));
            }
            set {
                this["supportEmail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("7")]
        public int specialPriceCalc {
            get {
                return ((int)(this["specialPriceCalc"]));
            }
            set {
                this["specialPriceCalc"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("images\\logo.png")]
        public string logoPath {
            get {
                return ((string)(this["logoPath"]));
            }
            set {
                this["logoPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("+45 2043 9925")]
        public string supportPhone {
            get {
                return ((string)(this["supportPhone"]));
            }
            set {
                this["supportPhone"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tmp\\")]
        public string pdfSavePath {
            get {
                return ((string)(this["pdfSavePath"]));
            }
            set {
                this["pdfSavePath"] = value;
            }
        }
    }
}
