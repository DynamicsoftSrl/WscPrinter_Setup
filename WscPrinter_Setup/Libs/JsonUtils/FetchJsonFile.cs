using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WscPrinter_Setup.Libs.JsonUtils {
  public static class FetchJsonFile {

    public static T LoadJson<T>(string fileName) {
      // deserialize JSON directly from a file
      string jsonString = File.ReadAllText(fileName);
      T item = JsonSerializer.Deserialize<T>(jsonString);

      return item;
    }
  }
}




