using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WscPrinter_Setup.ApiModels {
  public class UserProfile {
    public string SiteName { get; set; }
    public string TheSkin { get; set; }
    public string TheStyle { get; set; }
    public string UserEmail { get; set; }
    public string ReferenceWebsite { get; set; }
    public string ProductsSold { get; set; }
    public string CompanyType { get; set; }
    public string CompanyLogo { get; set; }
    public string CompanyLogoData { get; set; }
  }
}
