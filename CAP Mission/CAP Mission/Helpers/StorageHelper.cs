using CAPMission.Library.DataModel;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;


namespace CAPMission.Helpers
{
    public class StorageHelper
    {
        private const String MissionKey = "capmission";      
        
        private ISettings MissionSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }
        public void SaveVariable(string key, string value)
        {
            MissionSettings.AddOrUpdateValue(key, value);
        }
        public string GetVariable(string key)
        {
            return MissionSettings.GetValueOrDefault(key,"");
        }
        public void DeleteVariable(string key)
        {
            MissionSettings.Remove(key);
        }

        public void SaveMission(Mission mission)
        {
            MissionSettings.AddOrUpdateValue(MissionKey, JsonConvert.SerializeObject(mission));
        }
        public Mission GetCurrentMission()
        {
            var rtnMission =  JsonConvert.DeserializeObject<Mission>(MissionSettings.GetValueOrDefault(MissionKey,""));
            if (rtnMission == null)
                return new Mission();
            else
                return rtnMission;
        }

        public void DeleteCurrentMission()
        {
            MissionSettings.Remove(MissionKey);
        }
    }
}
