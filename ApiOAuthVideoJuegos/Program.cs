using ApiOAuthVideoJuegos.Data;
using ApiOAuthVideoJuegos.Helpers;
using ApiOAuthVideoJuegos.Models;
using ApiOAuthVideoJuegos.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("AzureServer");
builder.Services.AddSingleton<HelperOAuthToken>();
HelperOAuthToken helper = new HelperOAuthToken(builder.Configuration);
//A�ADIR LAS OPCIONES DE AUTENTICACION
builder.Services.AddAuthentication(helper.GetAuthenticationOptions()).AddJwtBearer(helper.GetJwtOptions());
builder.Services.AddTransient<RepositoryVideoJuegos>();
builder.Services.AddTransient<RepositoryUsuariosGaming>();
builder.Services.AddDbContext<VideoJuegosContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Title = "Apii OAuth Videojuegos 2023",
//        Version = "v1",
//        Description = "Api Videojuegos con seguridad token"
//    });
//});


builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Empleados";
    document.Description = "Api Timers 2023. Ejemplo OAuth";

    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
    // PERMITE A�ADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' as�: Bearer {Token JWT}."
        }
    );

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseSwagger();

app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/css/bootstrap.css");
    options.InjectStylesheet("/css/monokai.css");
    //options.InjectStylesheet("/css/material3x.css");
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    options.RoutePrefix = "";
    options.DocExpansion(DocExpansion.None);
});

//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Api Crud Videojuegos");
//    options.RoutePrefix = "";
//});

if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
