using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class UserSettingsViewModel : ViewModelBase
    {
        public bool SARFunctions
        {
            get
            {
                return Preferences.Get(nameof(SARFunctions), false);
            }
            set
            {
                Preferences.Set(nameof(SARFunctions), value);
                RaisePropertyChanged(nameof(SARFunctions));
            }
        }

        public UserSettingsViewModel(INavigation navigation) : base(navigation)
        {

        }
    }
}
