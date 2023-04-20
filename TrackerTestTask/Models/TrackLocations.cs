using TrackerTestTask.Data;

namespace TrackerTestTask.Models
{
    public class TrackLocations
    {
        public int Id { get; set; }
        public string IMEI { get; set; }
        
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DateEvent { get; set; }
        public DateTime DateTrack { get; set; }
        public int TypeSource { get; set; }

    }
}
