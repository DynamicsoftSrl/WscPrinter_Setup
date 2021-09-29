using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WscPrinter_Setup {
  public class Startup {
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }
    public ILogger<Startup> Logger { get; set; }
    public int mvcMode { get; set; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
      this.Environment = environment;
      this.Configuration = configuration;
      //
      // ========================================================================
      // 0 -> addMvc
      // 1 -> addRazorPages and AddController
      // 2 -> all of the above
      // ========================================================================
      //
      this.mvcMode = 2; // 0, // 1, // 2
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      // https://damienbod.com/2016/07/13/injecting-configurations-in-razor-views-in-asp-net-core/
      // services.Configure<IConfiguration>(Configuration);

      // seems ncessary for the injectionof the logger...
      services.AddLogging(config => config.AddSimpleConsole());

      //
      // session state, quick solution: config
      services.AddDistributedMemoryCache();
      services.AddSession(options =>
      {
        options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
      });




      // 
      // mvc-razor-api test start
      // .net services add test:
      // 
      IMvcBuilder MvcBuilder;
      switch (this.mvcMode) {
        case 0:
          // add Mvc includes all modes
          // TODO: 20210826 - check if really necessary (check MVC configure section comment)
          // TODO update: 20210920 - this is something to test the TODO...
          // TODO update: 20210920 - after addRazorMappin correction, the application as is now (razor and swagger) works with:
          //                         . AddMvc
          //                         . Add Controllers and addRazorPages
          //                         . both
          MvcBuilder = services.AddMvc();
          break;
        case 1:
          services.AddControllers();
          MvcBuilder = services.AddRazorPages();
          break;
        case 2:
        default:
          services.AddControllers();
          MvcBuilder = services.AddRazorPages();
          services.AddMvc();
          break;
      }

      MvcBuilder.AddRazorPagesOptions(options => {
        options.RootDirectory = "/Pages";
        options.Conventions.AddPageRoute("/cicca", "/Pages/Home");
      });
      if (Environment.IsDevelopment()) {
        MvcBuilder.AddRazorRuntimeCompilation();
      }
      MvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
      MvcBuilder.AddDataAnnotationsLocalization();
      // 
      // mvc-razor-api test end
      // 


      // 
      // for localization
      services.AddLocalization(options => {
        options.ResourcesPath = Configuration.GetValue<string>("i18n:LocalizationResourcePath"); // Props.LocalizationResourcePath;
      });


      services.Configure<RequestLocalizationOptions>(options => {
        // options.SetDefaultCulture(Configuration.GetValue<string>("i18n:DefaultCulture")); // Props.DefaultCulture);
        options.DefaultRequestCulture = new RequestCulture(
            culture: Configuration.GetValue<string>("i18n:DefaultCulture"),
            uiCulture: Configuration.GetValue<string>("i18n:DefaultCulture")

        ); // Props.DefaultCulture

        // https://stackoverflow.com/questions/42858335/how-to-hardcode-and-read-a-string-array-in-appsettings-json
        // options.SupportedCultures = string[];
        // options.SupportedUiCultures = string[];
        // options.FallBackToParentUICultures = true;
        var section = Configuration.GetSection("i18n:ActiveCultures"); //  Props.ActiveCultures);
        var cults = section.Get<string[]>();
        options.AddSupportedUICultures(cults);
        options.AddSupportedCultures(cults);

        // options
        //        .RequestCultureProviders
        //          .Remove((IRequestCultureProvider)typeof(AcceptLanguageHeaderRequestCultureProvider));

      });
      
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WscPrinter_Setup", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
      app.UseRequestLocalization(locOptions.Value);

      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WscPrinter_Setup v1"));
      }

      // uncomment to force https redirection
      // app.UseHttpsRedirection();


      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      // session state enabler
      app.UseSession();



      app.UseEndpoints(endpoints => {

        // 2 razor
        endpoints.MapRazorPages();
        var testResult = endpoints.MapControllers();

        // 3 controllers
        /* var testResult2 = endpoints.MapControllerRoute(
          name: "language",
          pattern: "{controller=LanguageManagerController}/{action=Language}/{id?}"
        );*/
      });
    }
  }
}
