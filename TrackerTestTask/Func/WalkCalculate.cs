using System.Reflection.Metadata.Ecma335;
using TrackerTestTask.Models;

namespace TrackerTestTask.Func
{
    public class WalkCalculate
    {
        public static List<WalkInfo> GetWalks(List<TrackLocations> trackLocations, int interval)
        {
            // разделяем записи на прогулки, условие - interval > 30 минут
            var walks = new List<WalkInfo>();
            var currentWalk = new List<object>();
            for (int i = 0; i < trackLocations.Count; i++)
            {
                if (currentWalk.Count == 0 || (DateTime)trackLocations[i].DateTrack - (DateTime)currentWalk.Last().GetType().GetProperty("DateTrack").GetValue(currentWalk.Last()) > TimeSpan.FromMinutes(interval))
                {
                    if (currentWalk.Count > 0 && DurationWalk.DurationGreatThenZero(currentWalk))
                    {
                        decimal startLatitude = (decimal)currentWalk.First().GetType().GetProperty("Latitude").GetValue(currentWalk.First());
                        decimal endLatitude = (decimal)currentWalk.Last().GetType().GetProperty("Latitude").GetValue(currentWalk.Last());
                        decimal startLongitude = (decimal)currentWalk.First().GetType().GetProperty("Longitude").GetValue(currentWalk.First());
                        decimal endLongitude = (decimal)currentWalk.Last().GetType().GetProperty("Longitude").GetValue(currentWalk.Last());

                        //проверяем было ли пройдено расстояние за прогулку
                        if (Distance.GetDistance(startLatitude, endLatitude, startLongitude, endLongitude) > 0)
                        {
                            // Добавляем прогулку список walks
                            walks.Add(new WalkInfo
                            {   
                                Date = trackLocations[i].DateTrack,
                                Duration = DurationWalk.GetDuration(currentWalk),
                                WalkDistance = Distance.GetDistance(startLatitude, endLatitude, startLongitude, endLongitude)
                            });
                        }
                    }
                    currentWalk = new List<object>();
                }
                currentWalk.Add(trackLocations[i]);
                
            }
            return walks;
        }
        
    }
}
