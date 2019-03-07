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
	public partial class SortieNotesView : ContentPage
	{
		public SortieNotesView (string inNoteNumber = null)
		{
			InitializeComponent ();
            BindingContext = new SortieNotesViewModel(Navigation, inNoteNumber);
		}

        private void Editor_Unfocused(object sender, FocusEventArgs e)
        {

        }
    }
}