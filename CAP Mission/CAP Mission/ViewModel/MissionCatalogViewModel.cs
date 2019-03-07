using CAPMission.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class MissionCatalogViewModel : ViewModelBase
    {
        private ICommand makeActiveCommand;
        private ICommand addMissionCommand;
        private ICommand misionDeleteCommand;
        private string selectedMission;
        private string newMissionNumber;
        private bool canAddMission;
        private Mission newMission;
        #region Properties
        public string SelectedMission
        {
            get => selectedMission;
            set
            {
                if (selectedMission != value)
                    selectedMission = value;
                RaisePropertyChanged("CanMakeActive");
            }
        }
        public bool CanMakeActive
        {
            get
            {
                return (SelectedMission != null);
            }
        }
        public bool CanAddMission
        {
            get => canAddMission;
            set
            {
                canAddMission = value;
                RaisePropertyChanged("CanAddMission");
            }
        }
        public string NewMissionNumber
        {
            get => newMissionNumber;
            set
            {
                newMissionNumber = value;
                CheckCanAddMission();
            }
        }
        #endregion
        public ICommand MakeActiveCommand { get => makeActiveCommand; }
        public ICommand AddMissionCommand { get => addMissionCommand; }
        public ICommand MisionDeleteCommand { get => misionDeleteCommand; }
        public MissionCatalogViewModel(INavigation navigation) : base(navigation)
        {
            makeActiveCommand = new Command(MakeMissionActive);
            addMissionCommand = new Command(AddMission);
            misionDeleteCommand = new Command<object>(MissionDeleteCommandExecute);
            LoadCurrentMission();
        }
        private void MakeMissionActive()
        {
            SwapToNewMission(selectedMission);
            RaiseAllPropertiesChanged();
        }
        private void AddMission()
        {
            newMission = new Mission();
            newMission.MissionNumber = NewMissionNumber;
            SaveMissionToList(newMission);
            if (CurrentMission == null || CurrentMission.MissionNumber == null)
                SwapToNewMission(newMission.MissionNumber);
            NewMissionNumber = "";
            RaiseAllPropertiesChanged();
        }
        private async void MissionDeleteCommandExecute(object missionNumber)
        {
            if (await DisplayYesNoDiaglog("Misison Delete","This will delete the selected mission and all related sorties.  Do you want to do that?"))
            {
                RemoveMissionFromList(missionNumber.ToString());
                if (CurrentMission.MissionNumber == missionNumber.ToString())
                {
                    if (MissionList.Count > 0)
                        SwapToNewMission(MissionList[0].MissionNumber);
                    else
                    {
                        CurrentMission = null;
                        ExecSaveMissionCommand();
                    }
                }
                selectedMission = null;
                RaiseAllPropertiesChanged();
            }
        }
        private void CheckCanAddMission()
        {
            if (newMissionNumber != null && newMissionNumber.Length > 0)
            {
                if (MissionList == null || !(MissionList.Any(s => s.MissionNumber == NewMissionNumber)))
                    CanAddMission = true;
                else
                    CanAddMission = false;
            }
            else
                CanAddMission = false;
        }
    }
}
