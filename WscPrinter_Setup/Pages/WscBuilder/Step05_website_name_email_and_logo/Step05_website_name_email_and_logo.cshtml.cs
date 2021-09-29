using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step05_website_name_email_and_logo {
  public class Step05WebsiteNameEmailAndLogo : PageModel {
    public UserProfile theWalkingUser { get; set; }

    public void OnGet() {
    }
    public void OnPost(UserProfile customer) {
      this.theWalkingUser = SessionHelper.GetObjectFromJson<UserProfile>(HttpContext.Session, "USER_PROFILE", new UserProfile());
      this.theWalkingUser.ReferenceWebsite = customer.ReferenceWebsite;
      SessionHelper.SetObjectAsJson(HttpContext.Session, "USER_PROFILE", this.theWalkingUser);
    }
  }
}
