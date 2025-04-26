using ApiProyectoBackPeluqueria.Data;
using ApiProyectoBackPeluqueria.Helpers;
using ApiProyectoBackPeluqueria.Repositories;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Azure.Identity; // si necesitas autenticación DefaultAzureCredential

var builder = WebApplication.CreateBuilder(args);

// Registrar clientes de Azure (KeyVault)
builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient(builder.Configuration.GetSection("KeyVault"));
});

// Configurar Helper de OAuth
HelperActionServicesOAuth helper = new HelperActionServicesOAuth(builder.Configuration);
builder.Services.AddSingleton(helper);

builder.Services.AddAuthentication(helper.GetAuthenticateSchema())
    .AddJwtBearer(helper.GetJwtBearerOptions());

builder.Services.AddTransient<RepositoryPeluqueria>();

// Configurar AppDbContext de forma correcta
builder.Services.AddDbContext<AppDbContext>(async (serviceProvider, options) =>
{
    var secretClient = serviceProvider.GetRequiredService<SecretClient>();

    KeyVaultSecret secretConnectionString = await secretClient.GetSecretAsync("sqlpeluqueria");

    options.UseSqlServer(secretConnectionString.Value);
});

// Configurar controladores y OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // opcional
}

app.MapOpenApi();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/openapi/v1.json", "ApiProyectoBackPeluqueria V1");
    c.RoutePrefix = "";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
