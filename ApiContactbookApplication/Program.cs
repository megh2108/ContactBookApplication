using ApiContactbookApplication;
using ApiContactbookApplication.Data;
using ApiContactbookApplication.Data.Contract;
using ApiContactbookApplication.Data.Implementation;
using ApiContactbookApplication.Services.Contract;
using ApiContactbookApplication.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowClientApiApplication", builder =>
    {
        builder.WithOrigins("http://localhost:5199").WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers();

//database connection
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("mydb"));
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

//register dependency
builder.Services.AddScoped<IContactbookService, ContactbookService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IVerifyPasswordHashService, VerifyPasswordHashService>();


builder.Services.AddScoped<IAppDbContext>(provider => (IAppDbContext)provider.GetService(typeof(AppDbContext)));
builder.Services.AddScoped<IContactbookRepository, ContactbookRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard authorization heading required using bearer scheme",
        In = ParameterLocation.Header,
        Name="Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseCors("AllowClientApiApplication");
app.UseAuthorization();




app.MapControllers();

app.Run();
