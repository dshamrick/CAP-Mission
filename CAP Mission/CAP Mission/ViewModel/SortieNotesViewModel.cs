using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using CAPMission.View;
using CAPMission.Library.DataModel;
using CAPMission.Library;
using System.Linq;
using System.Collections.ObjectModel;

namespace CAPMission.ViewModel
{
    public class SortieNotesViewModel : ViewModelBase
    {
        private ICommand markLocationCommand;
        private ICommand saveNoteCommand;
        private ICommand editMarkCommand;
        private string selectedSortieNumber;
        private SortieNote currentNote;
        private List<string> sortieNumbers;

        #region Properties
        public bool NewNote { get; set; }
        public bool AddButtonEnabled { get; set; }
        public List<string> SortieNumberList { get => sortieNumbers; }
        public ICommand MarkLocationCommand { get => markLocationCommand; }
        public ICommand SaveNoteCommand { get => saveNoteCommand; }
        public ICommand EditMarkCommand { get => editMarkCommand; }
        public ObservableCollection<MarkPoint> SortieMarkList { get => currentNote.Marks; }
        public string NoteEntry1
        {
            get
            {
                return currentNote.Note1;
            }
            set
            {
                currentNote.Note1 = value;
            }
        }
        public string NoteEntry2
        {
            get
            {
                return currentNote.Note2;
            }
            set
            {
                currentNote.Note2 = value;
            }
        }
        public string SelectedSortieNumber
        {
            get => selectedSortieNumber;
            set
            {
                selectedSortieNumber = value;
            }
        }
        #endregion
        public SortieNotesViewModel(INavigation navigation,string inNoteNumber = null) : base(navigation)
        {
            PendingEdits = false;
            LoadCurrentMission();
            if (inNoteNumber != null)
            {
                selectedSortieNumber = inNoteNumber;
                LoadSelectedSortieNote();
                AddButtonEnabled = false;
                NewNote = false;
            }
            else
            {
                currentNote = new SortieNote();
                AddButtonEnabled = true;
                NewNote = true;
            }

            markLocationCommand = new Command(ExecMarkLocation);
            saveNoteCommand = new Command(ExecSaveNoteCommand);
            editMarkCommand = new Command<object>(ExecEditMarkPoint);
        }
        private void LoadSelectedSortieNote()
        {
            var selected = CurrentMission.SortieNotes.FirstOrDefault(sn => sn.Number == SelectedSortieNumber);
            if (selected != null)
            {
                currentNote = selected;
                RaiseAllPropertiesChanged();
            }
            else
            {
                currentNote = new SortieNote();
                currentNote.Number = SelectedSortieNumber;
            }
        }
        private async void ExecMarkLocation()
        {
            try
            {
                //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                //var location = Geolocation.GetLocationAsync(request).Result;
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    int cnt = currentNote.Marks.Count;
                    MarkPoint point = new MarkPoint();
                    point.Name = "USR" + (cnt + 1).ToString("00");
                    point.Latitiude = location.Latitude;
                    point.Longitude = location.Longitude;
                    point.LatDegDM = GPSConvert.ConvertToDMS(Math.Abs(location.Latitude).ToString());
                    point.LongDegDM = GPSConvert.ConvertToDMS(Math.Abs(location.Longitude).ToString());
                    ShowEditMarkForm(point, true);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                DisplayDialog("Error", "There was an error attempting to access the Locatin Service.  Please check your permissions"); 
            }
        }
        private void ExecSaveNoteCommand()
        {
            var selected = CurrentMission.SortieNotes.FirstOrDefault(sn => sn.Number == SelectedSortieNumber);
            if (selected == null)
                CurrentMission.SortieNotes.Add(currentNote);
            else
            {
                int itemId = CurrentMission.SortieNotes.IndexOf(selected);
                CurrentMission.SortieNotes[itemId] = currentNote;
            }
            PendingEdits = false;
            ExecSaveMissionCommand();
            base.ExecuteCancelCommand();
        }
        private void ExecEditMarkPoint(Object point)
        {
            var lookupPoint = (MarkPoint)point;
            ShowEditMarkForm(lookupPoint, false);
        }
        private async void ShowEditMarkForm(MarkPoint point, bool newPoint)
        {
            MarkPointView pointEntryPage = new MarkPointView(point);
            pointEntryPage.Disappearing += (o, e) =>
            {
                if (pointEntryPage.VM.CurrentPoint != null)
                {
                    PendingEdits = true;
                    if (newPoint)
                        currentNote.Marks.Add(pointEntryPage.VM.CurrentPoint);
                    else
                    {
                        int i = currentNote.Marks.IndexOf(point);
                        currentNote.Marks[i] = pointEntryPage.VM.CurrentPoint;
                    }
                    RaisePropertyChanged("SortieMarkList");
                }
            };
            await Navigation.PushModalAsync(pointEntryPage);
        }
        protected override async void ExecuteCancelCommand()
        {
            if (PendingEdits)
            {
                if (await DisplayYesNoDiaglog("Pending Changes", "You have pending changes. Do you want to save them before you close this page."))
                    ExecSaveNoteCommand();
            }
            else
                base.ExecuteCancelCommand();
        }
    }
}
