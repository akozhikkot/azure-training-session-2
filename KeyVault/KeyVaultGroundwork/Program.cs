using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KeyVaultGroundwork;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SecretProviderService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var keyVaultName = configuration.GetValue<string>("KeyVaultName");

    var secretClient =
        new SecretClient(
            new Uri($"https://{keyVaultName}.vault.azure.net"),
            new DefaultAzureCredential());
    return new SecretProviderService(secretClient);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();