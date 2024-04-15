using HouseBuyingOrRenting.Application;
using HouseBuyingOrRenting.Application.Services;
using HouseBuyingOrRenting.Domain;
using HouseBuyingOrRenting.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var corsPolicyName = "RealEstateOrigins";

var supportedCultures = new[] { "vi-VN", "en-US" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)    
    .AddSupportedUICultures(supportedCultures);

var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

// Add services to the container.
builder.Services.Configure<HashPasswordOptions>(builder.Configuration.GetSection("HashPassword"));
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var error = context.ModelState.Values.SelectMany(x => x.Errors);

            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                TraceId = "",
                MoreInfo = "",
                ErrorData = error
            });
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "Authentication";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;

    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resource";
});

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(corsPolicyName);

//app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
