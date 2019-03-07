using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class MissionNumberEntryViewModel : ViewModelBase
    {
        private ICommand deleteMissionCommand;

        public ICommand DeleteMissionCommand { get => deleteMissionCommand; }
        public MissionNumberEntryViewModel(INavigation navigation) : base(navigation)
        {
            LoadCurrentMission();
            deleteMissionCommand = new Command(DeleteCurrentMission);
        }

        protected override void ExecSaveMissionCommand()
        {
            base.ExecSaveMissionCommand();
            Navigation.PopModalAsync();
        }

        private async void DeleteCurrentMission()
        {
            var answer = await DisplayYesNoDiaglog("Confirm", "Do you want to delete this mission and all of it's sorties?"); 
            if (answer)
                StorageHelper.DeleteCurrentMission();
            await Navigation.PopModalAsync();
        }
    }
}
