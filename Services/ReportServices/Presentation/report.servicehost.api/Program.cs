using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using report.application;
using report.persistence;
using report.persistence.Context;
using report.servicehost.api.Extensions;
using report.servicehost.api.OAuth;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Report Rest Full Service",
        Description = "Rise ",
        TermsOfService = new Uri("https://muhammeteser.com.tr"),
        Contact = new OpenApiContact
        {
            Name = "Muhammet ESER",
            Email = "esermuhammet@hotmail.com",
            Url = new Uri("https://muhammeteser.com.tr"),
        },
        License = new OpenApiLicense
        {
            Name = "Muhammet ESER",
            Url = new Uri("https://muhammeteser.com.tr"),
        }
    });
    //kod belgeleyici yolu
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
    c.IncludeXmlComments(xmlPath);
});
builder.Services.ConfigureConsul(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
IHostApplicationLifetime lifetime = app.Lifetime; 
app.RegisterWithConsul(lifetime);
app.Run();
