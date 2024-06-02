using Fiap.TesteTecnico.ClassManager.Api.Configurations;
using Fiap.TesteTecnico.ClassManager.Api.ExceptionHandler;
using Fiap.TesteTecnico.ClassManager.Infra;
using Fiap.TesteTecnico.ClassManager.Service;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddConnectionFactory();
builder.Services.AddRepositories();
builder.Services.AddMediatR();
builder.Services.AddValidators();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddCorsDefaultPolicy("CorsPolicy");

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

MigrationApplier.ApplyMigrations(app.Configuration);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
