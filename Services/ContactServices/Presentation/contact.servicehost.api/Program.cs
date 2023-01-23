using FluentValidation.AspNetCore;
using contact.application;  
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using contact.persistence;
using contact.servicehost.api.OAuth;
using contact.persistence.Context;
using contact.application.Handlers.Persons.ValidationRules;
using contact.infrastructure.Attributes;
using contact.servicehost.api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
               .AddFluentValidation(configuration => configuration 
                   .RegisterValidatorsFromAssemblyContaining<CreatePersonsCommandsValidator>()
                   .RegisterValidatorsFromAssemblyContaining<UpdatePersonsCommandsValidator>()
                   .RegisterValidatorsFromAssemblyContaining<DeletePersonsCommandsValidator>()
                   )
               .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Person Rest Full Service",
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
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Auth olmak için Bearer tokený burda kullanýnýz",
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
