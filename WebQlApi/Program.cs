using Application;
using ExternalEvents;
using HandlersQlApp;
using Infrastructure.Kafka;
using Infrastructure.Marten;
using Infrastructure.Persistence;
using Infrastructure.QL;
using InternalEvents;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddPersistenceServices(builder.Configuration);
services.AddKafkaConsumerServices(builder.Configuration);
services.AddApplicationServices();
services.AddAppQLMediatorServices();
services.AddExternalEventsServices();
services.AddInternalEventsServices();
services.AddSwaggerGen();
services.AddQLServices();
services.AddMartenServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UsePersistenceConfiguration();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseQlConfiguration(app.Environment.IsDevelopment());
app.Run();