using CleanArchitecture.Infrastructure.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if (UseAspire)
builder.AddServiceDefaults();
#endif

builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
await app.InitialiseDatabaseAsync();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#if (!UseAspire)
app.UseHealthChecks("/health");
#endif
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseExceptionHandler(options => { });

app.Map("/", () => Results.Redirect("/scalar/v1"));

#if (UseAspire)
app.MapDefaultEndpoints();
#endif
app.MapEndpoints();

app.Run();

public partial class Program { }
