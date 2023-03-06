using Hangfire;
using Hangfire.PostgreSql;
using Shared.ServiceConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Services.
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwagger();
services.AddAuth();

services
    .AddHangfire(config => config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")))
    .AddHangfireServer();

services.AddCors(options => options.AddPolicy("OpenCorsPolicy", corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

// App
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    AppPath = null
});

app.UseCors("OpenCorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();