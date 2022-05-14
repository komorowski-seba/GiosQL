namespace Application.Interfaces;

public interface IExternalEventService<in T>
    where T: class, IExternalEvent
{
    Task Publish(T evt);
}