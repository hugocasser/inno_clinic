﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.OperationResult {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class HttpErrorMessages {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HttpErrorMessages() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Application.OperationResult.Resources.HttpErrorMessages", typeof(HttpErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string ProfileNotFound {
            get {
                return ResourceManager.GetString("ProfileNotFound", resourceCulture);
            }
        }
        
        internal static string InternalServerError {
            get {
                return ResourceManager.GetString("InternalServerError", resourceCulture);
            }
        }
        
        internal static string SpecializationNotFound {
            get {
                return ResourceManager.GetString("SpecializationNotFound", resourceCulture);
            }
        }
        
        internal static string UserWithThisEmailNotExist {
            get {
                return ResourceManager.GetString("UserWithThisEmailNotExist", resourceCulture);
            }
        }
        
        internal static string OfficeNotFound {
            get {
                return ResourceManager.GetString("OfficeNotFound", resourceCulture);
            }
        }
        
        internal static string PhotoNotFound {
            get {
                return ResourceManager.GetString("PhotoNotFound", resourceCulture);
            }
        }
        
        internal static string RegistrationFailed {
            get {
                return ResourceManager.GetString("RegistrationFailed", resourceCulture);
            }
        }
        
        internal static string StatusNotExist {
            get {
                return ResourceManager.GetString("StatusNotExist", resourceCulture);
            }
        }
        
        internal static string Unauthorized {
            get {
                return ResourceManager.GetString("Unauthorized", resourceCulture);
            }
        }
    }
}
