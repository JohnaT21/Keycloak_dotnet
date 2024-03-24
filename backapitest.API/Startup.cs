using backapitest.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Supabase;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Configuration;
using System.Text;
using AspNetCore.KeycloakAuthentication;
using backapitest.Interface;
using Microsoft.IdentityModel.Logging;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        IdentityModelEventSource.ShowPII = true;
        var supabaseUrl = Configuration["Supabase:Url"];
        var supabaseApiKey = Configuration["Supabase:ApiKey"];
     
        // Add authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // expose the local 9001 in ngrok to use on flutter
                options.Authority = "https://a17c-91-242-248-223.ngrok-free.app/realms/flutter_dotNet"; // baseurl/realms/realmName
                options.Audience = "codex"; // client_id
                
                options.RequireHttpsMetadata = false; // For development purposes; use true in production.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Set the clock skew to zero for better accuracy
                };
            });
        
 
        services.AddAuthorization();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });



        // Add authorization
        services.AddAuthorization();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            // Configure Swagger to use JWT authentication
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter your JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
        });


        services.AddScoped<ISupabaseService, SupabaseService>(); // for demo purpose will convert to DI using Autofac
        services.AddScoped<IStoreService, StoreService>();
        services.AddSingleton(_ => new Supabase.Client(
            supabaseUrl,
            supabaseApiKey,
            new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            }));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
                // Configure Swagger to use JWT authentication
                c.InjectStylesheet("/path-to-your-stylesheet/custom.css");
                c.InjectJavascript("/path-to-your-javascript/custom.js");
                c.DocumentTitle = "Your API Documentation";
            });
        }

        //app.UseHttpsRedirection();

        // Enable authentication
        app.UseAuthentication();

        app.UseRouting();

        // Enable authorization
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
