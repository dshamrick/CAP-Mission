using CAPMission.Library.DataModel;
using CAPMission.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAPMission.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapMarkedPointView : ContentPage
	{
        MapMarkedPointViewModel vm;
		//public MapMarkedPointView (SortieNote inNote)
		//{
		//	InitializeComponent ();
  //          BindingContext = new MapMarkedPointViewModel(Navigation, inNote);
		//}

        public MapMarkedPointView(MarkPoint inNote)
        {
            InitializeComponent();
            vm = new MapMarkedPointViewModel(Navigation, inNote);
            vm.PointMap = MyMap;
            BindingContext = vm;            
        }
    }
}