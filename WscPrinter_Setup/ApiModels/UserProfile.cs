using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WscPrinter_Setup.ApiModels {
  public class UserProfile {
    string SiteName { get; set; }
    string TheSkin { get; set; }
    string TheStyle { get; set; }
    string UserEmail { get; set; }
    string ReferenceWebsite { get; set; }
    string ProductsSold { get; set; }
    string CompanyType { get; set; }
    string CompanyLogo { get; set; }
  }
}
