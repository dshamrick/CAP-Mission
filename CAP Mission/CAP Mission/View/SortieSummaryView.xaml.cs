using CAPMission.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAPMission.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SortieSummaryView : ContentPage
	{
		public SortieSummaryView ()
		{
			InitializeComponent ();
            BindingContext = new SortieSummaryViewModel(Navigation);
		}
    }
}