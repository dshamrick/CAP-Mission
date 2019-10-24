using CAPMission.Helpers;
using CAPMission.Library.DataModel;
using CAPMission.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        private Mission currentMission;
        private ICommand cancelCommand;
        private ICommand saveMissionCommand;
        private static List<Mission> missionList;
        private List<Aircraft> aircraftList;
        protected bool PendingEdits;

        public event PropertyChangedEventHandler PropertyChanged;

        protected static StorageHelper storageHelper;
        public const string ConvertListKey = "convertlist";
        //Used to get the Mission List from storage
        protected const string MissionListKey = "missionList";
        protected const string AircraftListKey = "aircraftList";
        public INavigation Navigation { get; set; }
        #region Properties
        public List<Aircraft> AircraftList
        {
            get
            {
                if (aircraftList == null)
                {
                    aircraftList = JsonConvert.DeserializeObject<List<Aircraft>>(StorageHelper.GetVariable(AircraftListKey));
                    if (aircraftList == null)
                        aircraftList = new List<Aircraft>();
                }
                return aircraftList;
            }
        }
        public static List<Mission> MissionList
        {
            get
            {
                if (missionList == null)
                {
                    missionList = JsonConvert.DeserializeObject<List<Mission>>(StorageHelper.GetVariable(MissionListKey));
                    if (missionList == null)
                        missionList = new List<Mission>();
                }
                return missionList;
            }
        }
        public Mission CurrentMission
        {
            get { return currentMission; }
            set
            {
                currentMission = value;
                RaisePropertyChanged("CurrentMission");
            }
        }
        public string CurrentMissionNumber
        {
            get
            {
                if (currentMission != null)
                    return currentMission.MissionNumber;
                else
                    return "";
            }
            set
            {
                if (value != null && currentMission.MissionNumber != value)
                {
                    SwapToNewMission(value);
                    currentMission.MissionNumber = value;
                    RaiseAllPropertiesChanged();
                }
            }
        }
        public List<string> MissionCatalogList
        {
            get
            {
                if (MissionList != null)
                    return MissionList.Select(ml => ml.MissionNumber).ToList();
                else
                    return null;
            }
        }
        protected static StorageHelper StorageHelper
        {
            get
            {
                if (storageHelper == null)
                    storageHelper = new StorageHelper();
                return storageHelper;
            }
        }
        #endregion
        public ICommand CancelCommand { get => cancelCommand; }
        public ICommand SaveMissionCommand { get => saveMissionCommand; }
        public ViewModelBase(INavigation navigation)
        {
            PendingEdits = false;
            Navigation = navigation;
            cancelCommand = new Command(ExecuteCancelCommand);
            saveMissionCommand = new Command(ExecSaveMissionCommand);
        }
        protected void LoadCurrentMission()
        {
            CurrentMission = StorageHelper.GetCurrentMission();
        }
        protected void SaveMissionCatalog()
        {
            StorageHelper.SaveVariable(MissionListKey, JsonConvert.SerializeObject(MissionList));
        }
        protected void UpdateMissionInList(Mission currentMission)
        {
            var itemIndex = MissionList.FindIndex(ml => ml.MissionNumber == currentMission.MissionNumber);
            if (itemIndex >= 0)
                MissionList[itemIndex] = currentMission;
            SaveMissionCatalog();
        }
        protected virtual void AddAircraftToList(Aircraft ac)
        {
            AircraftList.Add(ac);
            StorageHelper.SaveVariable(AircraftListKey, JsonConvert.SerializeObject(AircraftList));
        }
        protected void SaveMissionToList(Mission currentMission)
        {
            MissionList.Add(currentMission);
            SaveMissionCatalog();
        }
        protected void RemoveMissionFromList(string missionNumber)
        {
            var itemIndex = MissionList.FindIndex(ml => ml.MissionNumber == missionNumber);
            missionList.RemoveAt(itemIndex);
            SaveMissionCatalog();
        }
        protected async void ModalNavigation(ContentPage page)
        {
            StorageHelper.SaveMission(CurrentMission);
            page.Disappearing += Page_Disappearing;
            await Navigation.PushModalAsync(page);
        }
        protected virtual void SwapToNewMission(string newMissionNumber)
        {
            if (CurrentMission != null)
                UpdateMissionInList(CurrentMission);
            CurrentMission = MissionList.FirstOrDefault(ms => ms.MissionNumber == newMissionNumber);
            ExecSaveMissionCommand();
        }
        protected virtual void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected virtual void RaiseAllPropertiesChanged()
        {
            RaisePropertyChanged("");
        }
        protected virtual void Page_Disappearing(object sender, EventArgs e)
        {
            CurrentMission = StorageHelper.GetCurrentMission();
            RaisePropertyChanged("");
        }
        protected async Task<bool> DisplayYesNoDiaglog(string title, string message)
        {
            var answer = await Application.Current.MainPage.DisplayAlert(title, message, "Yes", "No");
            return answer;
        }
        protected async void DisplayDialog(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        }
        #region Common Commands
        protected virtual async void ExecuteCancelCommand()
        {
            await Navigation.PopModalAsync();
        }
        protected virtual void ExecSaveMissionCommand()
        {
            StorageHelper.SaveMission(CurrentMission);
        }
        protected virtual async Task MapMarkCommand(MarkPoint currentPoint)
        {
            Location loc = new Location();
            loc.Latitude = currentPoint.Latitiude;
            loc.Longitude = currentPoint.Longitude;
            var placemark = new Placemark();
            await Map.OpenAsync(loc, new MapLaunchOptions() { Name = currentPoint.Name });
        }
        #endregion
        protected event TimeUpdatedHandler TimeUpdatedEvent;
        public delegate void TimeUpdatedHandler(DateTime timeUpdateResult);
        protected virtual void OnTimeUpdated(DateTime timeUpdateResult)
        {

        }
    }
}
