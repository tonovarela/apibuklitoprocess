using apiBukLitoprocess.Clases;
using apiBukLitoprocess.conf;
using apiBukLitoprocess.Data;
using apiBukLitoprocess.repository.implementation;
using apiBukLitoprocess.repository.interfaces;
using apiBukLitoprocess.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient<RestClientService>();
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped<ColaboradorService>();

builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

builder.Services.AddHttpClient(ApiClientNames.Buk, (sp, client) =>
{
    var settings = builder.Configuration.GetSection("BukApiSettings").Get<ApiSettings>()!;
    client.BaseAddress = new Uri(settings.Url_API);
    client.DefaultRequestHeaders.Add("auth_token", settings.Token);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient(ApiClientNames.Asistencia, (sp, client) =>
{
    var settings = builder.Configuration.GetSection("AsistenciaApiSettings").Get<ApiSettings>()!;
    client.BaseAddress = new Uri(settings.Url_API);
    client.DefaultRequestHeaders.Add("token", settings.Token);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Registrar RestClientService que usa IHttpClientFactory
builder.Services.AddScoped<RestClientService>();

//builder.Services.AddHostedService<OutBoxWorker>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(
    builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();

app.Run();
