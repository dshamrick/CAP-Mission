using CAPMission.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private ICommand settingsCommand;
        private ICommand gpsConvertCommand;
        private ICommand sortieCommand;
        private ICommand sortieReportCommand;
        private ICommand sortieNoteCommand;
        private ICommand missionCatalogCommand;
        private ICommand miscCommand;
        public bool MissionButtonsEnabled
        {
            get
            {
                if (CurrentMission != null &&  CurrentMission.MissionNumber != null && CurrentMission.MissionNumber.Length > 0)
                    return true;
                else
                    return false;
            }
        }
        public ICommand SettingsCommand { get => settingsCommand; }
        public ICommand GPSConvertCommand { get => gpsConvertCommand; }
        public ICommand SortieCommand { get => sortieCommand; }
        public ICommand SortieReportCommand { get => sortieReportCommand; }
        public ICommand SortieNoteCommand { get => sortieNoteCommand; }
        public ICommand MissionCatalogCommand { get => missionCatalogCommand; }
        public ICommand MiscCommand { get => miscCommand; }
        public MainPageViewModel(INavigation navigation) : base(navigation)
        {
            LoadCurrentMission();
            settingsCommand = new Command(ExecSettingsCommand);
            gpsConvertCommand = new Command(ExecGPSConvertCommand);
            sortieCommand = new Command(ExecSortieCommand);
            sortieReportCommand = new Command(ExecSortieReportCommand);
            sortieNoteCommand = new Command(ExecSortieNoteCommand);
            missionCatalogCommand = new Command(ExecMissionCatalogCommand);
            miscCommand = new Command(ExecMiscFunction);
        }

        private void ExecSettingsCommand()
        {            
            ModalNavigation(new UserSettingsView());
        }
        private void ExecGPSConvertCommand()
        {
            //Notification note = new Notification();
            //note.Message = "Testing User Notifications";
            //CrossNotifications.Current.Send(note);
            ModalNavigation(new GPSConvertView());
        }
        private void ExecSortieCommand()
        {
            ModalNavigation(new SortieSummaryView());
        }
        private void ExecSortieReportCommand()
        {
            ModalNavigation(new SortieReportView());
        }
        private void ExecSortieNoteCommand()
        {
            ModalNavigation(new SortieNotesView());
        }
        private void ExecMissionCatalogCommand()
        {
            ModalNavigation(new MissionCatalogView());
        }
        private void ExecMiscFunction()
        {
            StorageHelper.DeleteVariable(AircraftListKey);
        }
    }
}
