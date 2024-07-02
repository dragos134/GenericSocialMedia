using GenericSocialMedia.Application;
using GenericSocialMedia.Application.Hubs;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Extensions;
using GenericSocialMedia.Persistence;
using GenericSocialMedia.Persistence.Context;
using GenericSocialMedia.Persistence.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();
builder.Services.AddHttpClient();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
//builder.Services.AddSingleton<IConfiguration>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICometChatService, CometChatService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", bld =>
        bld.WithOrigins(builder.Configuration["FrontendUrl"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
builder.Services.AddSwaggerGen(
    setupAction =>
{
    setupAction.SwaggerDoc("v1", new OpenApiInfo { Title = "GenericSocialMedia", Version = "v1" });
    //setupAction.IncludeXmlComments(xmlCommentsFullPath);
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.AddSecurityDefinition("GenericSocialMediaBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "GenericSocialMediaBearerAuth" }
            }, new List<string>() }
    });
}
);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))

        };
    });

var app = builder.Build();
StripeConfiguration.ApiKey = builder.Configuration["Stripe:ApiKey"];
var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
dataContext?.Database.EnsureCreated();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
//}

app.UseErrorHandler();
app.UseCors("CorsPolicy");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chatHub");

app.Run();
