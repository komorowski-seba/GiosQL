using Domain.Common.Interfaces;

namespace Domain.Events;

public class SetAddressStreetStationEvent : IEvent
{
    public Guid Id { get; }
    public string AddressStreet { get; }
    
    public SetAddressStreetStationEvent(Guid id, string addressStreet)
    {
        Id = id;
        AddressStreet = addressStreet;
    }
}