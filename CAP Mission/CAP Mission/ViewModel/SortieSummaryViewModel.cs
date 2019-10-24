                                 using CAPMission.Library.DataModel;
using CAPMission.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class SortieSummaryViewModel : ViewModelBase
    {
        private ICommand addNewSortieCommand;
        private ICommand saveNewSortieCommand;
        private ICommand sortieSelectCommand;
        private ICommand closeCommand;
        private ICommand sortieDeleteCommand;
        private ICommand sortieNoteCommand;
        private Sortie newSortie;
        private bool canSaveNewSortie;
        private Sortie selectedSortie;
        private SortieAddContentView addNewSortie;
        private bool showSortieAddContent;
        private bool canAddNewSortie;
        private Aircraft selectedAircraft;
        private ObservableCollection<ListSortie> sortieList;
        #region AddSortie Content View Properties
        public bool CanSaveNewSortie
        {
            get { return canSaveNewSortie; }
            set
            {
                canSaveNewSortie = value;
                RaisePropertyChanged("CanSaveNewSortie");
            }
        }
        public Sortie NewSortie
        {
            get { return newSortie; }
        }
        public DateTime SortieDate
        {
            get { return newSortie.SortieDate; }
            set
            {
                newSortie.SortieDate = value;
                RaisePropertyChanged("SortieDate");
                NewSortieSavable();
            }
        }
        public string SortieNumber
        {
            get { return newSortie.Number; }
            set
            {
                newSortie.Number = value;
                RaisePropertyChanged("SortieNumber");
                NewSortieSavable();
            }
        }
        public string Tail
        {
            get { return newSortie.Tail; }
            set
            {
                newSortie.Tail = value;
                RaisePropertyChanged("Tail");
            }
        }
        public List<String> AircraftPickList
        {
            get
            {
                return AircraftList.Select(ac => ac.TailNumber).ToList <string>();
            }
        }
        public string SelectedAircraft
        {
            set
            {
                Tail = value;
            }
        }
        public ObservableCollection<ListSortie> SortieList
        {
            //get { return CurrentMission.Sorties; }
            get { return sortieList; }
        }
        public SortieAddContentView AddNewSortie
        {
            get
            {
                if (addNewSortie == null)
                {
                    addNewSortie = new SortieAddContentView();
                }
                return addNewSortie;
            }
        }
        public bool ShowSortieAddContent
        {
            get => showSortieAddContent;
            set
            {
                showSortieAddContent = value;
                RaisePropertyChanged("ShowSortieAddContent");
            }
        }
        public bool CanAddNewSortie
        {
            get => canAddNewSortie;
            set
            {
                canAddNewSortie = value;
                RaisePropertyChanged("CanAddNewSortie");
            }
        }
        #endregion

        public ICommand AddNewSortieCommand { get => addNewSortieCommand; }
        public ICommand SaveNewSortieCommand { get => saveNewSortieCommand; }
        public ICommand SortieSelectCommand { get => sortieSelectCommand; }
        public ICommand CloseCommand { get => closeCommand; }
        public ICommand SortieDeleteCommand { get => sortieDeleteCommand; }
        public ICommand SortieNoteCommand { get => sortieNoteCommand; }
        public SortieSummaryViewModel(INavigation navigation):base(navigation)
        {
            newSortie = new Sortie();
            ShowSortieAddContent = false;
            CanAddNewSortie = true;
            LoadCurrentMission();
            BuildSortieList();
            addNewSortieCommand = new Command(ExecAddNewSortie);
            saveNewSortieCommand = new Command(ExecSaveNewSortie, () => CanSaveNewSortie);
            sortieSelectCommand = new Command<object>(ExecSortieSelected);
            closeCommand = new Command(ExecCloseCommand);
            sortieDeleteCommand = new Command<object>(ExecSortieDelete);
            sortieNoteCommand = new Command<object>(ExecEditSortieNote);
        }
        private void BuildSortieList()
        {
            sortieList = new ObservableCollection<ListSortie>();
            foreach (Sortie ste in CurrentMission.Sorties)
            {
                ListSortie newListSortie = new ListSortie()
                {
                    Tail = ste.Tail,
                    SortieDate = ste.SortieDate,
                    Number = ste.Number
                };
                newListSortie.Notes = (CurrentMission.SortieNotes.Any(n => n.Number == ste.Number));
                sortieList.Add(newListSortie);
            }
            RaisePropertyChanged("SortieList");
        }
        private void ExecAddNewSortie()
        {
            if (newSortie == null)
                newSortie = new Sortie();
            else
            {
                string oldNumber = newSortie.Number;
                string oldTail = newSortie.Tail;
                DateTime oldDate = newSortie.SortieDate;
                newSortie = new Sortie()
                {
                    Number = oldNumber,
                    Tail = oldTail,
                    SortieDate = oldDate
                };
            }
            ShowSortieAddContent = true;
            RaiseAllPropertiesChanged();
            CanSaveNewSortie = false;
            CanAddNewSortie = false;
        }
        private void ExecSaveNewSortie()
        {
            if (CurrentMission.Sorties == null)
                CurrentMission.Sorties = new ObservableCollection<Sortie>();
            CurrentMission.Sorties.Add(NewSortie);
            ListSortie newListSortie = new ListSortie()
            {
                Tail = NewSortie.Tail,
                SortieDate = NewSortie.SortieDate,
                Number = NewSortie.Number,
                Notes = false
            };
            sortieList.Add(newListSortie);
            ExecSaveMissionCommand();
            RaisePropertyChanged("SortieList");
            ShowSortieAddContent = false;
            CanAddNewSortie = true;
        }
        public void  NewSortieSavable()
        {
            bool retValue = false;
            if (SortieNumber != null && SortieNumber.Length > 0)
            {
                if (CurrentMission.Sorties == null || !(CurrentMission.Sorties.Any(s => s.Number == SortieNumber)))
                {
                    retValue = true;
                }
            }
            CanSaveNewSortie = retValue;
        }
        private void ExecSortieSelected(object sortieNumber)
        {
            var sNumber = (ListSortie)sortieNumber;
            selectedSortie = CurrentMission.Sorties.FirstOrDefault(s => s.Number == sNumber.Number);
            if (selectedSortie != null)
            {
                ContentPage page = new SortieManagementView(selectedSortie);
                ModalNavigation(page);
            }
        }
        private async void ExecSortieDelete(object sortieNumber)
        {
            var answer = await DisplayYesNoDiaglog("Confirm", "Do you want to delete this sortie from this mission?"); 
            if (answer)
            {
                selectedSortie = CurrentMission.Sorties.FirstOrDefault(s => s.Number == sortieNumber.ToString());
                if (selectedSortie != null)
                {
                    CurrentMission.Sorties.Remove(selectedSortie);
                    ExecSaveMissionCommand();
                }
            }
            BuildSortieList();
        }
        private void ExecEditSortieNote(object sortieNumber)
        {
            ModalNavigation(new SortieNotesView(sortieNumber.ToString()));
        }
        private void ExecCloseCommand()
        {
            ShowSortieAddContent = false;
            CanSaveNewSortie = false;
            CanAddNewSortie = true;
        }
        protected override void Page_Disappearing(object sender, EventArgs e)
        {
            base.Page_Disappearing(sender, e);
            BuildSortieList();
            RaisePropertyChanged("SortieList");
        }
        protected override void SwapToNewMission(string newMissionNumber)
        {
            base.SwapToNewMission(newMissionNumber);
            BuildSortieList();
        }
    }
}
