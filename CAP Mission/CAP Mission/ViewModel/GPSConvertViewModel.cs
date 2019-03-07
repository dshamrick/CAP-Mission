using CAPMission.Library;
using CAPMission.Library.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class GPSConvertViewModel : ViewModelBase
    {
        private ICommand convertCommand;
        private ICommand clearCommand;
        private string convertedLat;
        private string convertedLong;

        public string LatEntered { get; set; }
        public string LongEntered { get; set; }

        #region Properties
        public ObservableCollection<GPSConvertedValue> ConvertedValues
        {
            get;set;
        }
        public string ConvertedLat
        {
            get
            {
                return convertedLat;
            }
            set
            {
                convertedLat = value;
                RaisePropertyChanged("ConvertedLat");
            }
        }
        public string ConvertedLong
        {
            get
            {
                return convertedLong;
            }
            set
            {
                convertedLong = value;
                RaisePropertyChanged("ConvertedLong");
            }
        }
        #endregion

        public ICommand ConvertCommand { get => convertCommand; }
        public ICommand ClearCommand { get => clearCommand; }
        public GPSConvertViewModel(INavigation navigation ): base(navigation)
        {
            try
            {
                convertCommand = new Command(ConvertValues);
                clearCommand = new Command(ExecClearList);
                ConvertedValues = JsonConvert.DeserializeObject<ObservableCollection<GPSConvertedValue>>(StorageHelper.GetVariable(ConvertListKey));
                if (ConvertedValues == null)
                    ConvertedValues = new ObservableCollection<GPSConvertedValue>();
            }
            catch (Exception ex)
            {

            }
        }

        private void ConvertValues()
        {
            bool validConvert = false;
            GPSConvertedValue cvt = new GPSConvertedValue();
            if (LatEntered != null && LatEntered.Length > 0)
            {
                ConvertedLat = GPSConvert.ConvertToDMS(LatEntered);
                cvt.LatDecDegrees = LatEntered;
                cvt.LatDegDM = ConvertedLat;
                validConvert = true;
            }
            if (LongEntered != null && LongEntered.Length > 0)
            {
                ConvertedLong = GPSConvert.ConvertToDMS(LongEntered);
                cvt.LongDecDegrees = LongEntered;
                cvt.LongDegDM = ConvertedLong;
                validConvert = true;
            }
            if (validConvert)
                ConvertedValues.Insert(0,cvt);
        }
        private void ExecClearList()
        {
            ConvertedValues = new ObservableCollection<GPSConvertedValue>();
            RaisePropertyChanged("ConvertedValues");
        }
        protected override void ExecuteCancelCommand()
        {
            if (ConvertedValues != null && ConvertedValues.Count > 0)
                StorageHelper.SaveVariable(ConvertListKey, JsonConvert.SerializeObject(ConvertedValues));
            else
                StorageHelper.DeleteVariable(ConvertListKey);
            base.ExecuteCancelCommand();
        }
    }
}
