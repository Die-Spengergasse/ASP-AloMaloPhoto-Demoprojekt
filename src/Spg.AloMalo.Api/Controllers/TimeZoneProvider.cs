namespace Spg.AloMalo.Api.Controllers
{
    public class ZonedTimeProvider : TimeProvider
    {
        private TimeZoneInfo _zoneInfo;

        public ZonedTimeProvider(TimeZoneInfo zoneInfo) : base()
        {
            _zoneInfo = zoneInfo ?? TimeZoneInfo.Local;
        }

        public override TimeZoneInfo LocalTimeZone => _zoneInfo;

        public static TimeProvider FromLocalTimeZone(TimeZoneInfo zoneInfo) =>
            new ZonedTimeProvider(zoneInfo);
    }
}
