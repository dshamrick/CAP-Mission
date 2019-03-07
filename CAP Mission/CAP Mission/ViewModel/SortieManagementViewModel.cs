using CAPMission.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using CAPMission.View;

namespace CAPMission.ViewModel
{
    class SortieManagementViewModel :ViewModelBase
    {
        private Sortie selectedSortie;
        private ICommand engineStartCommand;
        private ICommand engineStopCommand;
        private ICommand wheelsUpCommand;
        private ICommand wheelsDnCommand;
        private ICommand saveSortieCommand;
        private ICommand editESCommand;
        private ICommand editWheelsUpCommand;
        private ICommand editWheelsDnCommand;
        private ICommand editEngineStopCommand;
        private ICommand sortieNoteCommand;
        private string hobbsEnd;
        private string hobbsStart;
        private string tachEnd;
        private string tachStart;

        private Sortie SelectedSortie
        {
            get { return selectedSortie; }
            set
            {
                selectedSortie = value;
                RaiseAllPropertiesChanged();
            }
        }

        #region Properties
        public bool CanEngineStartCommand
        {
            get => (selectedSortie.EngineStart == DateTime.MinValue);
        }
        public bool CanWheelsUpCommand
        {
            get => (selectedSortie.WheelsUp == DateTime.MinValue);
        }
        public bool CanWheelsDNCommand
        {
            get => (selectedSortie.WheelsDown == DateTime.MinValue);
        }
        public bool CanEngineStopCommand
        {
            get => (selectedSortie.EngineStop == DateTime.MinValue);
        }
        public string SortieNumber
        {
            get { return selectedSortie.Number; }
        }
        public string SortieDate
        {
            get { return selectedSortie.SortieDate.ToShortDateString(); }
            set
            {
                if (selectedSortie.SortieDate.ToShortDateString() != Convert.ToDateTime(value).ToShortDateString())
                {
                    selectedSortie.SortieDate = Convert.ToDateTime(value);
                    PendingEdits = true;
                }
            }
        }
        public string TailNumber
        {
            get { return selectedSortie.Tail; }
            set
            {
                if (selectedSortie.Tail != value)
                {
                    selectedSortie.Tail = value;
                    PendingEdits = true;
                }
            }
        }
        public string HobbsEnd
        {
            get
            {
                return selectedSortie.EndHobbs.ToString();
            }
            set
            {
                if (hobbsEnd != value )
                {
                    hobbsEnd = value;
                    if (selectedSortie.EndHobbs.ToString() != value)
                        PendingEdits = true;
                }
            }
        }
        public string HobbsStart
        {
            get
            {
                return selectedSortie.StartHobbs.ToString();
            }
            set
            {
                if (hobbsStart != value)
                {
                    hobbsStart = value;
                    if (selectedSortie.StartHobbs.ToString() != value)
                        PendingEdits = true;
                }
            }
        }
        public string TachEnd
        {
            get
            {
                return selectedSortie.EndTach.ToString();
            }
            set
            {
                if (tachEnd != value)
                {
                    tachEnd = value;
                    if (selectedSortie.EndTach.ToString() != value)
                        PendingEdits = true;
                }
            }
        }
        public string TachStart
        {
            get
            {
                return selectedSortie.StartTach.ToString();
            }
            set
            {
                if (tachStart != value)
                {
                    tachStart = value;
                    if (selectedSortie.StartTach.ToString() != value)
                        PendingEdits = true;
                }
            }
        }
        public string HobbsTime
        {
            get
            {
                return (selectedSortie.EndHobbs - selectedSortie.StartHobbs).ToString("N1");
            }
        }
        public string TachTime
        {
            get
            {
                return (selectedSortie.EndTach - selectedSortie.StartTach).ToString("N1");
            }
        }

        public string EngineStartLabel
        {
            get
            {
                if (SelectedSortie.EngineStart > DateTime.MinValue)
                    return "Engine Start " + SelectedSortie.EngineStart.ToString("HH:mm");
                else
                    return "Engine Start";
            }
        }
        public string WheelsUpLabel
        {
            get
            {
                if (SelectedSortie.WheelsUp > DateTime.MinValue)
                    return "Wheels Up " + SelectedSortie.WheelsUp.ToString("HH:mm");
                else
                    return "Wheels Up";
            }
        }
        public string EngineStopLabel
        {
            get
            {
                if (SelectedSortie.EngineStop > DateTime.MinValue)
                    return "Engine Stop " + SelectedSortie.EngineStop.ToString("HH:mm");
                else
                    return "Engine Stop";
            }
        }
        public string WheelsDownLabel
        {
            get
            {
                if (SelectedSortie.WheelsDown > DateTime.MinValue)
                    return "Wheels Down " + SelectedSortie.WheelsDown.ToString("HH:mm");
                else
                    return "Wheels Down";
            }
        }
        #endregion

        #region Command Definitions
        public ICommand EngineStartCommand { get => engineStartCommand; }
        public ICommand EngineStopCommand { get => engineStopCommand; }
        public ICommand WheelsUpCommand { get => wheelsUpCommand; }
        public ICommand WheelsDnCommand { get => wheelsDnCommand; }
        public ICommand SaveSortieCommand { get => saveSortieCommand; }
        public ICommand EditESCommand { get => editESCommand; }
        public ICommand EditWheelsUpCommand { get => editWheelsUpCommand; }
        public ICommand EditWheelsDnCommand { get => editWheelsDnCommand; }
        public ICommand EditEngineStopCommand { get => editEngineStopCommand; }
        public ICommand SortieNoteCommand { get => sortieNoteCommand; }
        #endregion
        public SortieManagementViewModel(Sortie selectedSortie, INavigation navigation): base (navigation)
        {
            SelectedSortie = selectedSortie;
            LoadCurrentMission();
            engineStartCommand = new Command(ExecEngineStartCommand);
            engineStopCommand = new Command(ExecEngineStopCommand);
            wheelsUpCommand = new Command(ExecWheelsUpCommand);
            wheelsDnCommand = new Command(ExecWheelsDownCommand);
            saveSortieCommand = new Command(ExecSaveSortieCommand);
            editESCommand = new Command(ExecEditEngineStartCommand);
            editWheelsUpCommand = new Command(ExecEditWheelsUpCommand);
            editEngineStopCommand = new Command(ExecEditEngineStopCommand);
            editWheelsDnCommand = new Command(ExecEditWheelsDNCommand);
            sortieNoteCommand = new Command(ExecSortieNoteCommand);
            PendingEdits = false;
        }
        #region Sortie Time Commands
        private async void ExecEditEngineStartCommand()
        {
            TimeEntryView timeEntryPage = new TimeEntryView("Engine Start Time", selectedSortie.EngineStart);
            timeEntryPage.Disappearing += (o, e) =>
            {
                DateTime dt = Convert.ToDateTime(SelectedSortie.EngineStart.ToShortDateString());
                DateTime dt2 = dt + timeEntryPage.VM.Time;
                SelectedSortie.EngineStart = dt2;
                RaisePropertyChanged("EngineStartLabel");
                RaisePropertyChanged("CanEngineStartCommand");
            };
            await Navigation.PushModalAsync(timeEntryPage);
        }

        private async void ExecEditWheelsUpCommand()
        {
            TimeEntryView timeEntryPage = new TimeEntryView("Engine Start Time", selectedSortie.WheelsUp);
            timeEntryPage.Disappearing += (o, e) =>
            {
                DateTime dt = Convert.ToDateTime(SelectedSortie.EngineStart.ToShortDateString());
                DateTime dt2 = dt + timeEntryPage.VM.Time;
                SelectedSortie.WheelsUp = dt2;
                RaisePropertyChanged("WheelsUpLabel");
                RaisePropertyChanged("CanWheelsUPCommand");
            };
            await Navigation.PushModalAsync(timeEntryPage);
        }
        private async void ExecEditEngineStopCommand()
        {
            TimeEntryView timeEntryPage = new TimeEntryView("Engine Start Time", selectedSortie.EngineStop);
            timeEntryPage.Disappearing += (o, e) =>
            {
                DateTime dt = Convert.ToDateTime(SelectedSortie.EngineStop.ToShortDateString());
                DateTime dt2 = dt + timeEntryPage.VM.Time;
                SelectedSortie.EngineStop = dt2;
                RaisePropertyChanged("EngineStopLabel");
                RaisePropertyChanged("CanEngineStopCommand");
            };
            await Navigation.PushModalAsync(timeEntryPage);
        }
        private async void ExecEditWheelsDNCommand()
        {
            TimeEntryView timeEntryPage = new TimeEntryView("Engine Start Time", selectedSortie.WheelsDown);
            timeEntryPage.Disappearing += (o, e) =>
            {
                DateTime dt = Convert.ToDateTime(SelectedSortie.WheelsDown.ToShortDateString());
                DateTime dt2 = dt + timeEntryPage.VM.Time;
                SelectedSortie.WheelsDown = dt2;
                RaisePropertyChanged("WheelsDownLabel");
                RaisePropertyChanged("CanWheelsDNCommand");
            };
            await Navigation.PushModalAsync(timeEntryPage);
        }
        private void ExecEngineStartCommand()
        {
            SelectedSortie.EngineStart = DateTime.Now;
            RaisePropertyChanged("EngineStartLabel");
            RaisePropertyChanged("CanEngineStartCommand");
            ExecSaveSortieCommand();
        }
        private void ExecWheelsUpCommand()
        {
            SelectedSortie.WheelsUp = DateTime.Now;
            RaisePropertyChanged("WheelsUpLabel");
            ExecSaveSortieCommand();
        }
        private void ExecWheelsDownCommand()
        {
            SelectedSortie.WheelsDown = DateTime.Now;
            RaisePropertyChanged("WheelsDownLabel");
            ExecSaveSortieCommand();
        }
        private void ExecEngineStopCommand()
        {
            SelectedSortie.EngineStop = DateTime.Now;
            RaisePropertyChanged("EngineStopLabel");
            ExecSaveSortieCommand();
        }
        #endregion
        private void ExecSaveSortieCommand()
        {
            Sortie sortie = CurrentMission.Sorties.FirstOrDefault(s => s.Number == SelectedSortie.Number);
            if (sortie != null)
            {
                double holdValue;
                sortie.EngineStart = SelectedSortie.EngineStart;
                sortie.EngineStop = SelectedSortie.EngineStop;
                sortie.WheelsDown = SelectedSortie.WheelsDown;
                sortie.WheelsUp = SelectedSortie.WheelsUp;
                if (double.TryParse(hobbsStart, out holdValue))
                {
                    sortie.StartHobbs = holdValue;
                }                
                if (double.TryParse(hobbsEnd, out holdValue))
                {
                    sortie.EndHobbs = holdValue;
                }
                if (double.TryParse(tachEnd, out holdValue))
                {
                    sortie.EndTach = holdValue;
                }
                if (double.TryParse(tachStart, out holdValue))
                {
                    sortie.StartTach = holdValue;
                }
                if (sortie.Tail != selectedSortie.Tail)
                    sortie.Tail = selectedSortie.Tail;
                sortie.SortieDate = selectedSortie.SortieDate;

                SelectedSortie = sortie;
            }
            StorageHelper.SaveMission(CurrentMission);
            PendingEdits = false;
        }
        private void ExecSortieNoteCommand()
        {
            ModalNavigation(new SortieNotesView(SelectedSortie.Number));
        }
        protected override async void ExecuteCancelCommand()
        {
            if (PendingEdits)
            {
                if (await DisplayYesNoDiaglog("Pending Changes", "You have pending changes. Do you want to save them before you close this page."))
                    ExecSaveSortieCommand();
            }
            base.ExecuteCancelCommand();
        }
    }
}
