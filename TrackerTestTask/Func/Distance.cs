namespace TrackerTestTask.Func
{
    public class Distance
    {
        //Считаем расстояние по координатам
        public static double GetDistance(decimal startLatitude, decimal endLatitude, decimal startLongitude, decimal endLongitude)
        {
            double R = 6371; // Радиус Земли в километрах
            double deltaLatitude = ToRadians((double)(endLatitude - startLatitude));
            double deltaLongitude = ToRadians((double)(endLongitude - startLongitude));
            double meanLatitude = ToRadians((double)((startLatitude + endLatitude) / 2));

            double distance = 2 * R * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(deltaLatitude / 2), 2) + Math.Cos((double)startLatitude) * Math.Cos((double)endLatitude) * Math.Pow(Math.Sin(deltaLongitude / 2), 2)));

            return Math.Round(distance, 2);
        }

        // Переводим градусы в радианы
        public static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
