using ClientApiContactbookApplication;
using ClientApiContactbookApplication.Implementation;
using ClientApiContactbookApplication.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//configure jwt authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorization();

//register depndency

builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IJwtTokenHandler,JwtTokenHandler>();
builder.Services.AddHttpClient();


var app = builder.Build();

var env = app.Services.GetRequiredService<IWebHostEnvironment>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//Add the JwtToken Middelware
app.UseMiddleware<JwtTokenMiddleware>();

app.UseStaticFiles();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
    RequestPath = "/Uploads"
});

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
