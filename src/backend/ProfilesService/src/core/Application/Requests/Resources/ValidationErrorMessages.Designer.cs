﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Requests.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ValidationErrorMessages {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationErrorMessages() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Application.Requests.Resources.ValidationErrorMessages", typeof(ValidationErrorMessages).Assembly);
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
        
        internal static string FirstNameIsRequred {
            get {
                return ResourceManager.GetString("FirstNameIsRequred", resourceCulture);
            }
        }
        
        internal static string LastNameIsRequered {
            get {
                return ResourceManager.GetString("LastNameIsRequered", resourceCulture);
            }
        }
        
        internal static string PageNumberMustBeGraterThanZero {
            get {
                return ResourceManager.GetString("PageNumberMustBeGraterThanZero", resourceCulture);
            }
        }
        
        internal static string PageSizeMustBeGraterThanZero {
            get {
                return ResourceManager.GetString("PageSizeMustBeGraterThanZero", resourceCulture);
            }
        }
        
        internal static string UserIdIsRequered {
            get {
                return ResourceManager.GetString("UserIdIsRequered", resourceCulture);
            }
        }
        
        internal static string PageSettingsAreRequred {
            get {
                return ResourceManager.GetString("PageSettingsAreRequred", resourceCulture);
            }
        }
        
        internal static string FirstNameToLong {
            get {
                return ResourceManager.GetString("FirstNameToLong", resourceCulture);
            }
        }
        
        internal static string BirthdayIsRequred {
            get {
                return ResourceManager.GetString("BirthdayIsRequred", resourceCulture);
            }
        }
        
        internal static string LastNameToLong {
            get {
                return ResourceManager.GetString("LastNameToLong", resourceCulture);
            }
        }
        
        internal static string CareerStartedIsRequered {
            get {
                return ResourceManager.GetString("CareerStartedIsRequered", resourceCulture);
            }
        }
    }
}