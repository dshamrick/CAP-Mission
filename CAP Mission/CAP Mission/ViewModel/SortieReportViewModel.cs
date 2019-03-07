using CAPMission.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CAPMission.ViewModel
{
    public class SortieReportViewModel : ViewModelBase
    {
        #region Properties
        public ObservableCollection<Sortie> SortieList
        {
            get { return CurrentMission.Sorties; }
        }
        public SortieReportViewModel(INavigation navigation) : base(navigation)
        {
            LoadCurrentMission();
        }
        public string TotalHobbs
        {
            get { return CurrentMission.Sorties.Sum(s => Convert.ToDecimal(s.HobbsTime)).ToString(); }
        }
        public string TotalTach
        {
            get { return CurrentMission.Sorties.Sum(s => Convert.ToDecimal(s.TachTime)).ToString(); }
        }
        #endregion
    }
}
