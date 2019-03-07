using CAPMission.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAPMission.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SortieReportView : ContentPage
	{
		public SortieReportView ()
		{
			InitializeComponent ();
            BindingContext = new SortieReportViewModel(Navigation);
		}
	}
}