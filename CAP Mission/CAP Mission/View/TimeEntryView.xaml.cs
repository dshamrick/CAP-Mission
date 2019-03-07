using CAPMission.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAPMission.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeEntryView : ContentPage
	{
        public TimeEntryViewModel VM { get; set; }
        
        public TimeEntryView(string Label,DateTime selectTime )
		{
			InitializeComponent ();
            VM = new TimeEntryViewModel(Label, selectTime, Navigation);
            BindingContext = VM;
		}
	}
}