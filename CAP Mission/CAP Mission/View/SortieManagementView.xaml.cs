using CAPMission.Library.DataModel;
using CAPMission.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAPMission.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SortieManagementView : ContentPage
	{
		public SortieManagementView (Sortie sortie)
		{
			InitializeComponent ();
            BindingContext = new SortieManagementViewModel(sortie, Navigation);
		}
	}
}