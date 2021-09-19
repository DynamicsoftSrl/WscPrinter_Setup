using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
  
    public Startup(IConfiguration configuration, IWebHostEnvironment environment) {
      this.Environment = environment;
      this.Configuration = configuration;

    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {

      // https://damienbod.com/2016/07/13/injecting-configurations-in-razor-views-in-asp-net-core/
      // services.Configure<IConfiguration>(Configuration);

      // seems ncessary for the injectionof the logger...
      services.AddLogging(config => config.AddSimpleConsole());
      services.AddControllers();
      var MvcBuilder = services.AddRazorPages();
      MvcBuilder.AddRazorPagesOptions(options => {
        options.RootDirectory = "/Pages";
        // options.Conventions.AddPageRoute("/cicca", "/Pages/razorTestNoModel");
      });
      if (Environment.IsDevelopment()) {
        MvcBuilder.AddRazorRuntimeCompilation();
      }
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

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
