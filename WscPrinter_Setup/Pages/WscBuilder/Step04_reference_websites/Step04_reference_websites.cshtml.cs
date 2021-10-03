using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step04_reference_websites
{
  public class Step04ReferenceWebsitesModel : PageModel {
    private UserProfile theWalkingUser { get; set; }

    public void OnGet() {
    }
    public void OnPost(UserProfile customer) {
      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
      this.theWalkingUser.ProductsSold = customer.ProductsSold;
      this.theWalkingUser.OtherProductsSold = customer.OtherProductsSold;
      HttpContext.Session.SetObjectAsJson("USER_PROFILE", this.theWalkingUser);
    }
  }
}
