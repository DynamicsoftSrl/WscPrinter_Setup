using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WscPrinter_Setup.Pages.WscBuilder.TemplatePreview
{
    public class TemplatePreviewModel : PageModel
    {
        public string LinkUrl { get; set; }
        public void OnGet()
        {
        }
        public void OnPost(string thelink)
        {
            this.LinkUrl = thelink;
        }

    }
}
