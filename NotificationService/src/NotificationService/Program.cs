using Hangfire;
using NotificationService;
using NotificationService.Api.Extensions;
using NotificationService.Api.Middlewares;
using NotificationService.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApp(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddEndpoints();

var app = builder.Build();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseCors(c =>
    c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseSwagger();
app.UseSwaggerUI();
app.UseHangfireDashboard();

app.UseHangfireServer();

app.MapEndpoints();

app.Run();