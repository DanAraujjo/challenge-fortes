using System;
using System.Globalization;
using Fortes.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FortesApi
{
  public class Startup
  {
    private readonly JwtTokenOptions _options = new JwtTokenOptions();

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      ConfigureJwtTokenService(services);

      services.AddMvc()
      .AddJsonOptions(options =>
          {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.Culture = new CultureInfo("pt-BR");
            options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
          })
          .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseAuthentication();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseCors(x =>
                   {
                     x.AllowAnyHeader();
                     x.AllowAnyMethod();
                     x.AllowAnyOrigin();
                   });

      app.UseHttpsRedirection();
      app.UseMvc();
    }

    /// <summary />
    private void ConfigureJwtTokenService(IServiceCollection services)
    {
      //configuração do token JWT
      services.Configure<JwtTokenOptions>(options =>
      {
        options.Issuer = _options.Issuer;
        options.Audience = _options.Audience;
        options.SigningCredentials = new SigningCredentials(_options.SigningKey, SecurityAlgorithms.HmacSha256);
      });

      //dd metodo de autenticação do token JWT
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(cfg =>
          {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuer = true,
              ValidIssuer = _options.Issuer,

              ValidateAudience = true,
              ValidAudience = _options.Audience,

              ValidateIssuerSigningKey = true,
              IssuerSigningKey = _options.SigningKey,

              RequireExpirationTime = true,
              ValidateLifetime = true,

              ClockSkew = TimeSpan.Zero
            };
          });

      services.AddAuthorization();
    }
  }
}
