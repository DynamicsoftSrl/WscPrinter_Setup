using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WscPrinter_Setup.ApiClients;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.JsonUtils;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step07_choose_layout {
  public class Step07ChooseLayoutModel : PageModel {
    private UserProfile theWalkingUser { get; set;  }
    public List<WebsiteEntity> ResponseCacheAttributeFromServer { get; set; }

    public async Task OnGetAsync() {

      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
      string theSkin = HttpContext.Request.Query["skin"];
      this.theWalkingUser.TheSkin = theSkin;
      HttpContext.Session.SetObjectAsJson("USER_PROFILE", this.theWalkingUser);
      JsonConf TR = this.getApiConf();
      HttpClient theClient = new HttpClient();
      this.theWalkingUser.AuthToken = TR.ApiServerToken;
      string theUri = TR.EndpointsUrl + TR.ServicePath;
      UserProfile4Wsc theComObj = new UserProfile4Wsc(this.theWalkingUser);
      this.ResponseCacheAttributeFromServer = await WscBuilderServerEndPointsCallers.SendUserData(theComObj, theUri, theClient);

    }
    private JsonConf getApiConf() {
      // string _filePath = AppDomain.CurrentDomain.BaseDirectory;
      // TextReader tr = new StreamReader(_filePath);
      string _filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Endpoints.json";
      JsonConf TR = FetchJsonFile.LoadJson<JsonConf>(_filePath);
      return TR;
    }
  }

  class JsonConf {
    public string EndpointsUrl { get; set; }
    public string ApiServerToken { get; set; }
    public string ServicePath { get; set; }

  }
}
