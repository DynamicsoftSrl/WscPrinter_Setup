using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WscPrinter_Setup.Libs.Session {
  public static class SessionExtensions {
    public static void SetObjectAsJson(this ISession session, string key, object value) {
      var options = new JsonSerializerOptions { WriteIndented = true };
      string theSrzdObj = JsonSerializer.Serialize(value, options);
      session.SetString(key, theSrzdObj);
    }

    public static T GetObjectFromJson<T>(this ISession session, string key) {
      string value = session.GetString(key);
      return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }
    public static T GetObjectFromJson<T>(this ISession session, string key, T defaultObject) {
      string value = session.GetString(key);
      return value == null ? defaultObject : JsonSerializer.Deserialize<T>(value);
    }
  }
}


