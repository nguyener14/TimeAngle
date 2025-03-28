using Microsoft.AspNetCore.Http.HttpResults;

namespace TimeAngle.Services
{
    public class TimeAngleService
    {
        private readonly ILogger<TimeAngleService> _logger;
        public TimeAngleService(ILogger<TimeAngleService> logger)
        {
            _logger = logger;
        }

        public async Task<double> CalculateTimeAngle(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), "Hour must be between 0 and 23");

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minute must be between 0 and 59");

            return TimeCalcluation(hour, minute);
        }

        public async Task<double> CalculateTimeAngle(string time)
        {
            try
            {
                if (!TimeSpan.TryParse(time, out var parsedTime))
                {
                    throw new ArgumentException("Invalid time format. Expected format: HH:mm");
                }

                return TimeCalcluation(parsedTime.Hours, parsedTime.Minutes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to calculate time from input {time}", time);
                throw;
            }
        }

        private double TimeCalcluation(int hour, int minute)
        {
            //360 degrees / 12 hours = 30
            //360 degrees/ 60 minutes = 6 
            //30 degrees/ 60 minutes = 0.5

            hour %= 12;
            var hourAngle = (hour * 30) + (minute * 0.5);
            var minuteAngle = minute * 6;

            return hourAngle + minuteAngle;
        }
    }
}
