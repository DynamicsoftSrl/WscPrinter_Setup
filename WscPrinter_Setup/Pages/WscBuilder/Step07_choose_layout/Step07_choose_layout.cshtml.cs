using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step07_choose_layout {
  public class Step07ChooseLayoutModel : PageModel {
    private UserProfile theWalkingUser { get; set;  }

    public void OnGet() {

      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
      string theQuery = HttpContext.Request.Query["skin"];
      HttpContext.Session.SetObjectAsJson("USER_PROFILE", this.theWalkingUser);


    }
  }
}
