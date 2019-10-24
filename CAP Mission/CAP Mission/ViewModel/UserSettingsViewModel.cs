using CAPMission.Library.DataModel;
using CAPMission.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class UserSettingsViewModel : ViewModelBase
    {
        private ICommand myAircraftCommand;
        private ICommand hideAircraftCommand;

        private bool showAddAircraft;
        private AddAircraftContentView addNewAircraft;
        public AddAircraftContentView AddNewAircraft
        {
            get
            {
                if (addNewAircraft == null)
                {
                    addNewAircraft = new AddAircraftContentView();
                }
                return addNewAircraft;
            }
        }
        public bool ShowAddAircraft
        {
            get => showAddAircraft;
            set
            {
                showAddAircraft = value;
                RaisePropertyChanged(nameof(ShowAddAircraft));
            }
        }

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

        public ICommand MyAircraftCommand => myAircraftCommand;
        public ICommand HideAircraftCommand => hideAircraftCommand;
        public UserSettingsViewModel(INavigation navigation) : base(navigation)
        {
            myAircraftCommand = new Command(ShowMyAircraft);
            hideAircraftCommand = new Command(HideAircraft);
        }

        private void ShowMyAircraft()
        {
            ShowAddAircraft = true;
        }
        private void HideAircraft()
        {
            ShowAddAircraft = false;
        }
    }
}
