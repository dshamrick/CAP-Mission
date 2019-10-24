using CAPMission.Library.DataModel;
using CAPMission.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Xamarin.Forms.Maps;

namespace CAPMission.ViewModel
{
    public class MarkPointViewModel : ViewModelBase
    {
        private ICommand savePointCommand;
        private ICommand displayOnMapCommand;
        #region Properties
        public MarkPoint CurrentPoint { get; set; }
        public string Name
        {
            get { return CurrentPoint.Name; }
            set { CurrentPoint.Name = value; }
        }
        public string LatText
        {
            get
            {
                return "Lat:  " + CurrentPoint.LatDegDM;
            }
        }
        public string LongText
        {
            get
            {
                return "Lon:  " + CurrentPoint.LongDegDM;
            }
        }
        public string PointNote
        {
            get { return CurrentPoint.MarkNote; }
            set { CurrentPoint.MarkNote = value; }
        }
        #endregion
        public ICommand SavePointCommand { get => savePointCommand; }
        public ICommand DisplayOnMapCommand { get => displayOnMapCommand; }
        public MarkPointViewModel(MarkPoint point,INavigation navigation) : base(navigation)
        {
            LoadCurrentMission();
            CurrentPoint = point;
            savePointCommand = new Command(ExecSavePointCommand);
            displayOnMapCommand = new Command(ExecDisplayOnMap);
        }
        private void ExecSavePointCommand()
        {
            Navigation.PopModalAsync();
        }
        protected override void ExecuteCancelCommand()
        {
            CurrentPoint = null;
            base.ExecuteCancelCommand();
        }
        private async void ExecDisplayOnMap()
        {
            await MapMarkCommand(CurrentPoint);
        }
    }
}
