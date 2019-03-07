using CAPMission.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAPMission.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MissionCatalogView : ContentPage
	{
		public MissionCatalogView ()
		{
			InitializeComponent ();
            BindingContext = new MissionCatalogViewModel(Navigation);
		}
	}
}