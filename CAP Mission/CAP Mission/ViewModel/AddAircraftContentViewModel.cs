using CAPMission.Library.DataModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace CAPMission.ViewModel
{
    public class AddAircraftContentViewModel : ViewModelBase
    {
        private ICommand saveAircraftCommand;
        private ICommand deleteAircraftCommand;
        private string currentTail;
        private ObservableCollection<Aircraft> currentAicraftList;

        public ObservableCollection<Aircraft> CurrentAircraftList
        {
            get
            {
                if (currentAicraftList == null)
                    currentAicraftList = new ObservableCollection<Aircraft>();
                else
                    currentAicraftList.Clear();
                foreach(Aircraft ac in AircraftList)
                {
                    currentAicraftList.Add(ac);
                }
                return currentAicraftList;
            }
        }
        public string CurrentTail
        {
            get => currentTail;
            set
            {
                currentTail = value;
                RaisePropertyChanged(nameof(CurrentTail));
                RaisePropertyChanged(nameof(CanSaveAircraft));
            }
        }
        public bool CanSaveAircraft
        {
            get
            {
                if (CurrentTail != null && CurrentTail.Length > 0)
                    return true;
                else
                    return false;
            }
        }
        public ICommand SaveAircraftCommand => saveAircraftCommand;
        public ICommand DeleteAircraftCommand => deleteAircraftCommand;
        public AddAircraftContentViewModel(INavigation navigation) : base(navigation)
        {
            saveAircraftCommand = new Command(ExecSaveAircraftList);
            deleteAircraftCommand = new Command<object>(ExecAircraftDelete);
        }
        private void ExecSaveAircraftList()
        {
            int nextAc = 1;
            if (currentAicraftList.Count > 0)
            {
                nextAc = CurrentAircraftList.Max(x => x.Index);
                nextAc++;
            }
            Aircraft ac = new Aircraft() { TailNumber = CurrentTail, Index = nextAc };
            AddAircraftToList(ac);
            CurrentTail = string.Empty;
        }
        protected override void AddAircraftToList(Aircraft ac)
        {
            CurrentAircraftList.Add(ac);
            base.AddAircraftToList(ac);
        }
        private void ExecAircraftDelete(object obj)
        {
            Aircraft ac = (Aircraft)obj;
            AircraftList.Remove(ac);
            CurrentAircraftList.Remove(ac);
            StorageHelper.SaveVariable(AircraftListKey, JsonConvert.SerializeObject(AircraftList));
        }
    }
}
