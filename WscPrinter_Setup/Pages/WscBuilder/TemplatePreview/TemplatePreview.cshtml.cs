using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.TemplatePreview
{
    

    public class TemplatePreviewModel : PageModel
    {
        public string CustomerName { get; set; }
        public string LinkUrl { get; set; }
        public void OnGet()
        {
           
        }
        public void OnPost(string thelink)
        {
            var theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());

            this.CustomerName = theWalkingUser.SiteName;
            this.LinkUrl = thelink;
        }

    }
}
