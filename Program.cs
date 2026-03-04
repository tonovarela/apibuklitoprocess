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

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

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
