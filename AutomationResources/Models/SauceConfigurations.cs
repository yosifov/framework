namespace AutomationResources.Models
{
    using AutomationResources.Enums;

    public class SauceConfigurations
    {
        public SauceConfigurations(Browser browser,
                            string version,
                            string os,
                            string deviceName,
                            string deviceOrientation,
                            string screenResolution = "1280x1024")
        {
            this.Browser = browser;
            this.Version = version;
            this.Os = os;
            this.DeviceName = deviceName;
            this.DeviceOrientation = deviceOrientation;
            this.ScreenResolution = screenResolution;
        }

        public Browser Browser { get; }

        public string Version { get; }

        public string Os { get; }
        
        public string DeviceName { get; }
        
        public string DeviceOrientation { get; }
        
        public string ScreenResolution { get; }
    }
}
