using Database;
using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Config;
using WebApi.Service.CollectData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var connectString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ItemContext>(
    options => options.UseNpgsql(connectString)
    );

// 注册服务
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true, //是否验证Issuer
        ValidIssuer = configuration["Jwt:Issure"], //发行人Issuer
        ValidateAudience = true, //是否验证Audience
        ValidAudience = configuration["Jwt:Audience"], //订阅人Audience
        ValidateIssuerSigningKey = true, //是否验证SecurityKey
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)), //SecurityKey
        ValidateLifetime = true, //是否验证失效时间
        ClockSkew = TimeSpan.FromDays(1), //过期时间容错值，解决服务器端时间不同步问题（秒）
        RequireExpirationTime = true,
    };
});


builder.Services.AddSingleton(new JwtConfig(configuration));
builder.Services.AddScoped<GetSystemData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
