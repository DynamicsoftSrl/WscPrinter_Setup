using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WscPrinter_Setup.ApiModels;

namespace WscPrinter_Setup.ApiClients {
    public static class WscBuilderServerEndPointsCallers {
    public static async Task<List<WebsiteEntity>> SendUserData(UserProfile4Wsc theUser, string uri, HttpClient httpClient) {
      var myContent = JsonSerializer.Serialize<UserProfile4Wsc>(theUser);
      var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
      var byteContent = new ByteArrayContent(buffer);
      byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      using HttpResponseMessage httpResponse = await httpClient.PostAsync(uri, byteContent);

      var rspresp = httpResponse.Content;
      httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
      try {
        string theSerializedObj = await httpResponse.Content.ReadAsStringAsync();

        List<WebsiteEntity> theWebs = JsonSerializer.Deserialize<List<WebsiteEntity>>(theSerializedObj);
        return await Task.FromResult<List<WebsiteEntity>>(theWebs);
        
      } catch (Exception ex)// Could be ArgumentNullException or UnsupportedMediaTypeException
        {
        Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
      }
      return null;

    }
  }
}
