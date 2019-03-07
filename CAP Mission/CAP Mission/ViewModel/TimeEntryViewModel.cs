using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase
    {
        private TimeSpan  displayTime;
        public TimeSpan Time
        {
            get => displayTime;
            set { displayTime = value; }
        }
        public string PageLabel
        { get; set; }
        public TimeEntryViewModel(string label,DateTime inputTime,INavigation navigation) : base(navigation)
        {
            PageLabel = label;
            Time = inputTime.TimeOfDay;
        }
    }
}
