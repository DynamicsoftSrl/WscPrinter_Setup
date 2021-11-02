using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WscPrinter_Setup.Pages
{
    public class IndexModel : PageModel
    {
        public Boolean isYoutube = false;
        public void OnGet()
        {
            string isyoutube = Request.Query["youtube"];
            isyoutube = string.IsNullOrEmpty(isyoutube) ? "false" : isyoutube;
            this.isYoutube = isyoutube.ToLower() == "true";
        }
    }
}
