using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step02_company_type {
  public class Step02CompanyTypeModel : PageModel {
    private UserProfile theWalkingUser { get; set; }

    public void OnGet() {
    }
    public void OnPostSubmit(string customer_company_type) {
      this.theWalkingUser = SessionHelper.GetObjectFromJson<UserProfile>(HttpContext.Session, "USER_PROFILE", new UserProfile());
      this.theWalkingUser.CompanyType = customer_company_type;
      SessionHelper.SetObjectAsJson(HttpContext.Session, "USER_PROFILE", this.theWalkingUser);
    }
  }
}
