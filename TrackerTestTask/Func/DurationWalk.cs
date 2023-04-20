using System;

namespace TrackerTestTask.Func
{
    public class DurationWalk
    {
        public static string GetDuration(List<object> currentWalk)
        {
            TimeSpan duration = (DateTime)currentWalk.Last().GetType().GetProperty("DateTrack").GetValue(currentWalk.Last()) - (DateTime)currentWalk.First().GetType().GetProperty("DateTrack").GetValue(currentWalk.First());
            string durationStr = duration.ToString();
            int totalSeconds = (int)duration.TotalSeconds;
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
            durationStr = string.Format("{0:D2}:{1:D2}", (int)timeSpan.Hours, timeSpan.Minutes);

            return durationStr;
        }

        public static bool DurationGreatThenZero(List<object> currentWalk)
        {
            TimeSpan duration = (DateTime)currentWalk.Last().GetType().GetProperty("DateTrack").GetValue(currentWalk.Last()) - (DateTime)currentWalk.First().GetType().GetProperty("DateTrack").GetValue(currentWalk.First());
            string durationStr = duration.ToString();
            int totalSeconds = (int)duration.TotalSeconds;
            if(totalSeconds > 0)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        
    }
}
