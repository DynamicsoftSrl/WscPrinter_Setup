using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step06_choose_skin_and_styles {
  public class Step06ChooseSkinAndStylesModel : PageModel {
    public UserProfile theWalkingUser { get; set; }

    public void OnGet() {
    }
    public void OnPost(UserProfile customer) {
      this.theWalkingUser = SessionHelper.GetObjectFromJson<UserProfile>(HttpContext.Session, "USER_PROFILE", new UserProfile());
      this.theWalkingUser.SiteName = customer.SiteName;
      this.theWalkingUser.UserEmail = customer.UserEmail;
      this.theWalkingUser.CompanyLogo = customer.CompanyLogo;
      SessionHelper.SetObjectAsJson(HttpContext.Session, "USER_PROFILE", this.theWalkingUser);
    }
  }
}
