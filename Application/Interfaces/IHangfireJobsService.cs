namespace Application.Interfaces;

public interface IHangfireJobsService
{
    public void AllStationJob();
    public void AllStationsStatusJob();
}