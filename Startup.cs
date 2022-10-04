using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OidcClientDemoApplication
{
    public class Startup
    {

        public IWebHostEnvironment Environment {get; }
        public IConfiguration Configuration {get; }

        public Startup(IWebHostEnvironment environment, IConfiguration config) {
            Environment = environment;
            Configuration = config;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Prevent WS-Federation claim names being added to tokens
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            
            services.AddAuthentication(options => {
                
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                
                // This is the strongest setting in production and also supports HTTP on developer workstations
                options.Cookie.SameSite = SameSiteMode.Strict;
            })
            .AddOpenIdConnect(options => {

                // Use the same settings here
                options.NonceCookie.SameSite = SameSiteMode.Strict;
                options.CorrelationCookie.SameSite = SameSiteMode.Strict;
                
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority=Configuration.GetValue<string>("OpenIdConnect:Issuer");
                options.ClientId=Configuration.GetValue<string>("OpenIdConnect:ClientId");
                options.ClientSecret=Configuration.GetValue<string>("OpenIdConnect:ClientSecret");
                options.ResponseType = "code";
                options.SaveTokens = true;
                
                // Add scope to the request if configured; "openid profile" is default
                string scopeString = Configuration.GetValue<string>("OpenIDConnect:Scope");
                if (!String.IsNullOrEmpty(scopeString)) {
                    
                    options.Scope.Clear();
                    scopeString.Split(" ", StringSplitOptions.TrimEntries).ToList().ForEach(scope => {
                        options.Scope.Add(scope);
                    });
                }

                // Initial configuration is HTTP
                options.RequireHttpsMetadata = false;

                // For testing, enforce login even if user has an alive session with the Curity Identity Server
                options.Prompt = "login"; 

                /*options.BackchannelHttpHandler = new HttpClientHandler()
                {
                    Proxy = new WebProxy("http://127.0.0.1:8888"),
                    UseProxy = true,
                };

                options.Events.OnMessageReceived = ctx => {

                    System.Console.WriteLine("*** Message received");
                    System.Console.WriteLine(ctx.Response);
                    return Task.CompletedTask;
                };*/

            });

            services.AddAuthorization();
            services.AddRazorPages();
        }
    }
}
