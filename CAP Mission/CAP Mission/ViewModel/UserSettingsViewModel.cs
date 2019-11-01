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
        private ICommand saveNotificationCommand;
        protected ICommand startAlertCommand;

        private bool showAddAircraft;
        private AddAircraftContentView addNewAircraft;
        private AlertSetting localAlertSetting;

        #region Properties
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
                RaisePropertyChanged(nameof(EnableMyAircraft));
            }
        }
        public bool EnableMyAircraft
        {
            get => !showAddAircraft;
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
        public bool OpsNormal
        {
            get
            {
                return Preferences.Get(nameof(OpsNormal), false);
            }
            set
            {
                if (!value)
                {
                    ActivateNotifications(false);
                }
                Preferences.Set(nameof(OpsNormal), value);
                RaisePropertyChanged(nameof(OpsNormal));
            }
        }
        public string AlertMessage
        {
            get => localAlertSetting.AlertMessage;
            set
            {
                if (localAlertSetting.AlertMessage != value)
                {
                    localAlertSetting.AlertMessage = value;
                    PendingEdits = true;
                }
            }
        }
        public bool AlertTime
        {
            get
            {
                return localAlertSetting.AlertTime;
            }
            set
            {
                localAlertSetting.AlertTime = value;
                PendingEdits = true;
                RaisePropertyChanged(nameof(TimeDisplaced));
                RaisePropertyChanged(nameof(TimeBased));
            }
        }
        public bool TimeDisplaced
        {
            get => AlertTime;
        }
        public bool TimeBased
        {
            get => !AlertTime;
        }
        public bool Checked00
        {
            get => localAlertSetting.Checked00;
            set
            {
                localAlertSetting.Checked00 = value;
                PendingEdits = true;
            }
        }
        public bool Checked15
        {
            get => localAlertSetting.Checked15;
            set
            {
                localAlertSetting.Checked15 = value;
                PendingEdits = true;
            }
        }
        public bool Checked30
        {
            get => localAlertSetting.Checked30;
            set
            {
                localAlertSetting.Checked30 = value;
                PendingEdits = true;
            }
        }
        public bool Checked45
        {
            get => localAlertSetting.Checked45;
            set
            {
                localAlertSetting.Checked45 = value;
                PendingEdits = true;
            }
        }
        #endregion
        public ICommand MyAircraftCommand => myAircraftCommand;
        public ICommand HideAircraftCommand => hideAircraftCommand;
        public ICommand SaveNotificationCommand => saveNotificationCommand;
        public ICommand StartAlertCommand => startAlertCommand;
        public UserSettingsViewModel(INavigation navigation) : base(navigation)
        {
            myAircraftCommand = new Command(ShowMyAircraft);
            hideAircraftCommand = new Command(HideAircraft);
            saveNotificationCommand = new Command(SaveNotificationSettings);
            startAlertCommand = new Command(StartAlerts);
            LoadVariables();
            RaiseAllPropertiesChanged();
            PendingEdits = false;
        }

        private void ShowMyAircraft()
        {
            ShowAddAircraft = true;
        }
        private void HideAircraft()
        {
            ShowAddAircraft = false;
        }
        private void SaveNotificationSettings()
        {
            AlertSettings = localAlertSetting;
            PendingEdits = false;
        }
        private void LoadVariables()
        {
            localAlertSetting = AlertSettings;
        }
        protected override async void ExecuteCancelCommand()
        {
            if (PendingEdits)
            {
                if (await DisplayYesNoDiaglog("Pending Changes", "You have pending changes. Do you want to save them before you close this page."))
                    SaveNotificationSettings();
            }
            base.ExecuteCancelCommand();
        }
        private void StartAlerts()
        {
            ActivateNotifications(false);
        }
    }
}
