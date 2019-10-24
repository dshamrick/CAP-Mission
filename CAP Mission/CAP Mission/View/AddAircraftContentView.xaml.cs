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
    public partial class AddAircraftContentView : ContentView
    {
        public AddAircraftContentView()
        {
            InitializeComponent();
            BindingContext = new AddAircraftContentViewModel(Navigation);
        }
    }
}