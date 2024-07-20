using ContactBookApplication;
using ContactBookApplication.Data;
using ContactBookApplication.Data.Contract;
using ContactBookApplication.Data.Implementation;
using ContactBookApplication.Services.Contract;
using ContactBookApplication.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//database connection
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("contactdb"));
});

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


//configure service (Buisness layer )
//builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IContacbookService, ContacbookService>();

//configure service (Data  layer )
//builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IContactbookRepository, ContactbookRepository>();

var app = builder.Build();

//Inject IwebHostEnvironment
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//Add the JwtToken Middelware
app.UseMiddleware<JwtTokenMiddleware>();
app.UseStaticFiles();
//allow other folder to add in it
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Uploads")),
    RequestPath = "/Uploads"
});

app.UseRouting();

//tell that use authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


