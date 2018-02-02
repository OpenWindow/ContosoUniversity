using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContosoUniv.MvcClient.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using ContosoUniv.MvcClient.Infrastructure;

namespace ContosoUniv.MvcClient
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(options => {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
      })
      .AddCookie("cookie")
      .AddOpenIdConnect("oidc", options => 
      {
        options.Authority = "http://localhost:5000";
        options.ClientId = "openIdConnectClient";
        options.SignInScheme = "cookie";
        options.RequireHttpsMetadata = false;
      });

      services.AddDbContext<SchoolContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddMvc(opt =>
          {
            opt.Filters.Add(typeof(DbContextTransactionFilter));
                  // opt.ModelBinderProviders.Insert(0, new EntityModelBinderProvider());
                })
          .AddFeatureFolders();

      services.AddAutoMapper(typeof(Startup));

      services.AddMediatR(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
