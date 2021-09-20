using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WscPrinter_Setup.Controllers
{
  [Route("[controller]")]
  public class LanguageManagerController : Controller
  {

    public IConfiguration Configuration { get; }
    public ILogger Logger;
    public IWebHostEnvironment env;

    public LanguageManagerController(IConfiguration Configuration
                  , ILoggerFactory loggerFactory
                  , IWebHostEnvironment env)
    {
      this.Logger = loggerFactory.CreateLogger<LanguageManagerController>();
      this.Configuration = Configuration;
      this.env = env;
    }

    // GET: /<controller>/
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("LanguageManager/Language/{id?}")]
    public IActionResult Language(string id)
    {

      return View();
    }
    
    //
    //
    // =========================================================================================================
    //
    //                change language
    //


    [HttpPost, HttpGet]
    [Route("LanguageManager/SetLanguage/")]
    [Route("LanguageManager/SetLanguage/{culture?}/{returnUrl?}")]
    public IActionResult SetLanguage(string? culture, string? returnUrl)
    {
      Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
      );

      return LocalRedirect(returnUrl);
    }



  }
}
