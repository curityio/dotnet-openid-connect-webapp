using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Keep the token as it is and clear any mapping rules
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            
            services.AddAuthentication(options => {
                // Persist user data in cookie
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                // Use OpenId Connect for authentication
                options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            // Default cookie settings
            .AddCookie()
            // OpenId Connect client settings (integration with the Curity Identity Server)
            .AddOpenIdConnect(options => {               
                options.Authority=Configuration.GetValue<string>("OpenIdConnect:Issuer");
                options.ClientId=Configuration.GetValue<string>("OpenIdConnect:ClientId");
                options.ClientSecret=Configuration.GetValue<string>("OpenIdConnect:ClientSecret");

                options.ResponseType = "code";
                // Enforce login even if user has an alive session with the Curity Identity Server
                options.Prompt="login"; 
                options.SaveTokens=true;

                string scopeString = Configuration.GetValue<string>("OpenIDConnect:Scope");
                
                // Add scope to the request if configured; "openid profile" is default
                if (!String.IsNullOrEmpty(scopeString)) {
                    options.Scope.Clear();
                    string[] scopes = scopeString.Split(" ", StringSplitOptions.TrimEntries);
                    
                    foreach(string scope in scopes) {
                        options.Scope.Add(scope);
                    }
                }

                // Require https for all environments except during development
                options.RequireHttpsMetadata=!Environment.IsDevelopment();
            });
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Unauthenticated view
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                // Authenticated view
                endpoints.MapGet("/protected", async context =>
                {
                    // Get the logged-in user and related claims from the ID token
                    ClaimsPrincipal user = context.User;
                    await context.Response.WriteAsync(String.Format("Hello {0}! ", user.FindFirstValue("sub")));

                    // Get the access token required for upstream requests
                    String access_token = await context.GetTokenAsync("access_token");
                    await context.Response.WriteAsync(String.Format("This application received the following access token: {0}", access_token));
                }).RequireAuthorization();
                 
            });
        }
    }
}
