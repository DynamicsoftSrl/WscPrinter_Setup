using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Web.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using WscPrinter_Setup.ApiModels;
using WscPrinter_Setup.Libs.Session;

namespace WscPrinter_Setup.Pages.WscBuilder.Step05_website_name_email_and_logo {

  public class AppFile {
    public byte[] Content { get; set; }
    public string B64Content { get; set; }
  }

  public class BufferedSingleFileUploadObj {
    [Required]
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }
  }
  public class Step05WebsiteNameEmailAndLogoModel : PageModel {
    public UserProfile theWalkingUser { get; set; }

    [BindProperty]
    public BufferedSingleFileUploadObj FileUpload { get; set; }
 
    public void OnGet() {
    }
    public void OnPost(UserProfile customer) {
      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
      this.theWalkingUser.ReferenceWebsite = customer.ReferenceWebsite;
      HttpContext.Session.SetObjectAsJson("USER_PROFILE", this.theWalkingUser);
    }

    //
    // https://stackoverflow.com/questions/61223339/multiple-submit-buttons-in-razor-pages-not-able-to-find-handler-pagenamehandler
    public async Task /* Task<IActionResult> */ OnPostUploadAsync() {
      this.theWalkingUser = HttpContext.Session.GetObjectFromJson<UserProfile>("USER_PROFILE", new UserProfile());
      using (var memoryStream = new MemoryStream()) {
        await FileUpload.FormFile.CopyToAsync(memoryStream);

        string fileName = FileUpload.FormFile.FileName;

        // Upload the file if less than 2 MB
        if (memoryStream.Length < 2097152) {
          var file = new AppFile() {
            Content = memoryStream.ToArray()
          };
          file.B64Content = Convert.ToBase64String(file.Content);
          this.theWalkingUser.CompanyLogoData = file.B64Content;
          this.theWalkingUser.CompanyLogo = fileName;
          HttpContext.Session.SetObjectAsJson("USER_PROFILE", this.theWalkingUser);
        } else {
          ModelState.AddModelError("File", "The file is too large.");
        }
      }
      await Task.CompletedTask;
      // return Page();
    }


  }
}
