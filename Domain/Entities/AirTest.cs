using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class AirTest : IEntities
    {
        public Guid Id { get; private set; }
        public Guid StationId { get; private set; }
        public long StationIdentifier { get; private set; }
        public DateTime CalcDate { get; private set; }
        public DateTime DownloadDate { get; private set; }
        public int So2IndexLevel { get; private set; }
        public string So2IndexName { get; private set; }
        public int No2IndexLevel { get; private set; }
        public string No2IndexName { get; private set; }
        public int Pm10IndexLevel { get; private set; }
        public string Pm10IndexName { get; private set; }
        public int Pm25IndexLevel { get; private set; }
        public string Pm25IndexName { get; private set; }
        public int O3IndexLevel { get; private set; }
        public string O3IndexName { get; private set; }

        public Station Station { get; }

        public static AirTest Create(
            DateTime calcDate,
            DateTime downloadDate,
            int so2IndexLevel,
            string so2IndexName,
            int no2IndexLevel,
            string no2IndexName,
            int pm10IndexLevel,
            string pm10IndexName,
            int pm25IndexLevel,
            string pm25IndexName,
            int o3IndexLevel,
            string o3IndexName,
            Station station)
            => new AirTest(
                calcDate,
                downloadDate,
                so2IndexLevel,
                so2IndexName,
                no2IndexLevel,
                no2IndexName,
                pm10IndexLevel,
                pm10IndexName,
                pm25IndexLevel,
                pm25IndexName,
                o3IndexLevel,
                o3IndexName,
                station);
        
        private AirTest() { }
        
        private AirTest(
            DateTime calcDate,
            DateTime downloadDate, 
            int so2IndexLevel, 
            string so2IndexName, 
            int no2IndexLevel,
            string no2IndexName,
            int pm10IndexLevel,
            string pm10IndexName,
            int pm25IndexLevel,
            string pm25IndexName,
            int o3IndexLevel,
            string o3IndexName,
            Station station)
        {
            if (calcDate == null)
                throw new ArgumentNullException(nameof(calcDate));
            if (downloadDate == null)
                throw new ArgumentNullException(nameof(downloadDate));

            Id = Guid.NewGuid();
            CalcDate = calcDate;
            DownloadDate = downloadDate;
            So2IndexLevel = so2IndexLevel;
            So2IndexName = so2IndexName;
            No2IndexLevel = no2IndexLevel;
            No2IndexName = no2IndexName;
            Pm10IndexLevel = pm10IndexLevel;
            Pm10IndexName = pm10IndexName;
            Pm25IndexLevel = pm25IndexLevel;
            Pm25IndexName = pm25IndexName;
            O3IndexLevel = o3IndexLevel;
            O3IndexName = o3IndexName;
            
            Station = station ?? throw new ArgumentNullException(nameof(station));
            StationId = station.Id;
            StationIdentifier = station.Identifier;
        }
    }
}