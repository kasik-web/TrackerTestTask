namespace TrackerTestTask.Data
{
    public class Storage
    {
        private static string imei;

        public static void SaveImei(string newImei)
        {
            imei = newImei;
        }

        public static string GetImei()
        {
            return imei;
        }
    }
}
