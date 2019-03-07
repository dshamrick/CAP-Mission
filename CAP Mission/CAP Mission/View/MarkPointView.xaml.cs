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
	public partial class MarkPointView : ContentPage
	{
        public MarkPointViewModel VM;
        public MarkPointView (MarkPoint point)
		{
			InitializeComponent ();
            VM = new MarkPointViewModel(point, Navigation);
            BindingContext = VM;
		}
	}
}