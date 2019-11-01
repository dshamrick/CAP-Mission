using System;
using System.Collections.Generic;
using System.Text;

namespace CAPMission.Library.DataModel
{
    public class UserSettings
    {
        public List<string> TailNumbers { get; set; }
    }
    public class AlertSetting
    {
        public bool Checked00 { get; set; }
        public bool Checked15 { get; set; }
        public bool Checked30 { get; set; }
        public bool Checked45 { get; set; }
        public string AlertMessage { get; set; }
        /// <summary>
        /// If true base the alert time as a duratoin from the Engine Start time.  If false, use standard minute of the hour
        /// </summary>
        public bool AlertTime { get; set; }
        public DateTime LastAlertSent { get; set; }
        public DateTime EngineStart { get; set; }
    }
}
