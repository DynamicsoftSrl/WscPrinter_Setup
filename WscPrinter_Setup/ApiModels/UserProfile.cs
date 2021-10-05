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
    public string UserDeclaredName { get; set; }
    public string UserPhone { get; set; }
    public string[] ReferenceWebsite { get; set; }
    public string OtherReferenceWebsite { get; set; }
    public string[] ProductsSold { get; set; }
    public string OtherProductsSold { get; set; }
    public string[] CompanyType { get; set; }
    public string OtherCompanyType { get; set; }
    public string CompanyLogo { get; set; }
    public string CompanyLogoData { get; set; }
    public string AuthToken { get; set; }
  }

  public class UserProfile4Wsc {
    public string SiteName { get; set; }
    public string TheSkin { get; set; }
    public string TheStyle { get; set; }
    public string UserEmail { get; set; }
    public string UserDeclaredName { get; set; }
    public string UserPhone { get; set; }
    public string ReferenceWebsite { get; set; }
    public string OtherReferenceWebsite { get; set; }
    public string ProductsSold { get; set; }
    public string OtherProductsSold { get; set; }
    public string CompanyType { get; set; }
    public string OtherCompanyType { get; set; }
    public string CompanyLogo { get; set; }
    public string CompanyLogoData { get; set; }
    public string AuthToken { get; set; }
    public UserProfile4Wsc(UserProfile theWalkingObj) {
      this.ProductsSold = quickArrayToString(theWalkingObj.ProductsSold) ?? "";
      this.OtherProductsSold = theWalkingObj.OtherProductsSold ?? "";
      this.ReferenceWebsite = quickArrayToString(theWalkingObj.ReferenceWebsite) ?? "";
      this.OtherReferenceWebsite = theWalkingObj.OtherReferenceWebsite ?? "";
      this.CompanyType = quickArrayToString(theWalkingObj.CompanyType) ?? "";
      this.OtherCompanyType = theWalkingObj.OtherCompanyType ?? "";
      this.TheStyle = theWalkingObj.TheStyle ?? "";
      this.TheSkin = theWalkingObj.TheSkin ?? "";
      this.SiteName = theWalkingObj.SiteName ?? "";
      this.UserEmail = theWalkingObj.UserEmail ?? "";
      this.UserPhone = theWalkingObj.UserPhone ?? "";
      this.UserDeclaredName = theWalkingObj.UserDeclaredName ?? "";

      this.CompanyLogo = theWalkingObj.CompanyLogo ?? "";
      this.CompanyLogoData = theWalkingObj.CompanyLogoData ?? "";
      this.AuthToken = theWalkingObj.AuthToken;

    }

    private string quickArrayToString(string[] originalArrStrings) {
      string result = "";
      foreach (string kk in originalArrStrings) {
        result += "|" + kk;
      }
      return result.Substring(1);
    }
  }


}
