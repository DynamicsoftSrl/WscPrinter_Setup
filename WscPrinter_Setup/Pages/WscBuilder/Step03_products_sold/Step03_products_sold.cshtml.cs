using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step03_products_sold
{
  public class Step03ProductsSoldModel : PageModel
  {
    UserProfile theWalkingUser { get; set;  }
    public void OnGet() {
      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
    }

    public void OnPost(UserProfile customer) {
      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
      this.theWalkingUser.CompanyType = customer.CompanyType;
      this.theWalkingUser.OtherCompanyType = customer.OtherCompanyType;
      HttpContext.Session.SetObjectAsJson("USER_PROFILE", this.theWalkingUser);
    }
  }
}
