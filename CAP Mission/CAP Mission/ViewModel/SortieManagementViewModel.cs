﻿using CAPMission.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using CAPMission.View;
using Xamarin.Essentials;

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
        private ICommand startAlertCommand;

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
        public bool CanPickStartTimes
        {
            get => (CurrentMission.Sorties.Count > 1);
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
        public bool Instruction
        {
            get => selectedSortie.Instruction;
            set { selectedSortie.Instruction = value; }
        }
        public string SelectedAircraft
        {
            set
            {
                selectedSortie.Tail = value;
                RaisePropertyChanged(nameof(TailNumber));
            }
        }
        private bool showCopyFromList;
        public bool ShowCopyFromList
        {
            get => showCopyFromList;
            set
            {
                showCopyFromList = value;
                RaisePropertyChanged(nameof(ShowCopyFromList));
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
                    return "Engine Start:     " + SelectedSortie.EngineStart.ToString("HH:mm");
                else
                    return "Engine Start";
            }
        }
        public string WheelsUpLabel
        {
            get
            {
                if (SelectedSortie.WheelsUp > DateTime.MinValue)
                    return "Wheels Up:     " + SelectedSortie.WheelsUp.ToString("HH:mm");
                else
                    return "Wheels Up";
            }
        }
        public string EngineStopLabel
        {
            get
            {
                if (SelectedSortie.EngineStop > DateTime.MinValue)
                    return "Engine Stop:     " + SelectedSortie.EngineStop.ToString("HH:mm");
                else
                    return "Engine Stop";
            }
        }
        public string FlightTime
        {
            get
            {
                if (SelectedSortie.EstimateTime > 0)
                    return SelectedSortie.EstimateTime.ToString();
                else
                    return "0.0";
            }
        }
        public string Origin
        {
            get => selectedSortie.Origin;
            set
            {
                if (selectedSortie.Origin != value)
                {
                    selectedSortie.Origin = value.ToUpper();
                    PendingEdits = true;
                }
            }
        }
        public string Destination
        {
            get => selectedSortie.Destination;
            set
            {
                if (selectedSortie.Destination != value)
                {
                    selectedSortie.Destination = value.ToUpper();
                    PendingEdits = true;
                }
            }
        }
        public string Pax1
        {
            get => selectedSortie.Pax1;
            set
            {
                selectedSortie.Pax1 = value;
                PendingEdits = true;
            }
        }
        public string Pax2
        {
            get => selectedSortie.Pax2;
            set
            {
                selectedSortie.Pax2 = value;
                PendingEdits = true;
            }
        }
        public string Pax3
        {
            get => selectedSortie.Pax3;
            set
            {
                selectedSortie.Pax3 = value;
                PendingEdits = true;
            }
        }
        public string WheelsDownLabel
        {
            get
            {
                if (SelectedSortie.WheelsDown > DateTime.MinValue)
                    return "Wheels Down:     " + SelectedSortie.WheelsDown.ToString("HH:mm");
                else
                    return "Wheels Down";
            }
        }
        public List<string> CopySortieList
        {
            get
            {
                if (CurrentMission.Sorties.Count > 0)
                {
                    return CurrentMission.Sorties.Where(st => st.Tail == TailNumber && st.Number != SortieNumber).Select(s => s.Number).ToList<string>();
                }
                else
                    return null;
            }
        }
        public string SelectedStartSortie
        {
            get => ("Copy Start Times From?");
            set
            {
                Sortie sortie = CurrentMission.Sorties.FirstOrDefault(s => s.Number == value);
                if (sortie != null)
                {
                    if (sortie.EndHobbs > 0 && sortie.EndTach > 0)
                    {
                        selectedSortie.StartHobbs = sortie.EndHobbs;
                        selectedSortie.StartTach = sortie.EndTach;
                        RaisePropertyChanged(nameof(HobbsStart));
                        RaisePropertyChanged(nameof(TachStart));
                    }
                }
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
        public ICommand StartAlertCommand { get => startAlertCommand; }
        public ICommand CopyFromCommand { get; }
        #endregion
        public SortieManagementViewModel(Sortie selectedSortie, INavigation navigation): base (navigation)
        {
            ShowCopyFromList = false;
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
            startAlertCommand = new Command(ExecStartAlerts);
            CopyFromCommand = new Command(ExecCopyStartTime);
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
                sortie.Instruction = selectedSortie.Instruction;
                sortie.Origin = selectedSortie.Origin;
                sortie.Destination = selectedSortie.Destination;
                sortie.Pax1 = selectedSortie.Pax1;
                sortie.Pax2 = selectedSortie.Pax2;
                sortie.Pax3 = selectedSortie.Pax3;
                if (SelectedSortie.EngineStart > DateTime.MinValue && SelectedSortie.EngineStop > DateTime.MinValue)
                {
                    TimeSpan ts = SelectedSortie.EngineStop.Subtract(SelectedSortie.EngineStart);
                    int hrs = ts.Hours;
                    int mnts = ts.Minutes;
                    int tenths = mnts / 6;
                    string fltTime = hrs.ToString() + "." + tenths.ToString();
                    sortie.EstimateTime = Convert.ToDouble(fltTime);
                    RaisePropertyChanged(nameof(FlightTime));
                }

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
        private void ExecStartAlerts()
        {
            if (SelectedSortie.EngineStart > DateTime.MinValue)
                AlertSettings.EngineStart = SelectedSortie.EngineStart;
            ActivateNotifications(!AlertsActive);
            RaisePropertyChanged(nameof(AlertButtonText));
        }
        private void ExecCopyStartTime()
        {
            string str = "Test if we got here";
            ShowCopyFromList = true;
        }
    }
}
