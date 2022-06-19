using Application;
using ApplicationQlApp.ExternalEvents;
using ApplicationQlApp.InternalEvents;
using ApplicationQlApp.Mediator;
using Infrastructure;
using Infrastructure.Elastic;
using Infrastructure.Persistence;
using Infrastructure.QL;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseConfigSeriLog(builder.Configuration, builder.Environment.EnvironmentName);

var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddApplicationServices();
services.AddAppQLMediatorServices();
services.AddExternalEventsServices();
services.AddInternalEventsServices();
// services.AddSwaggerGen();
services.AddWebQlInfrastructureServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}
app.UsePersistenceConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseQlConfiguration(app.Environment.IsDevelopment());
app.Run();