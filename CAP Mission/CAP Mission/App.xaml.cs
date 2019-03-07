using CAPMission.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CAPMission
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
    /*
     * Development Log:
     * Time to Date: 40 hrs  01/15/2019
     * Includes:
     *          GPS Conversion Page
     *          Sortie Entry
     *          Sortie Details page Including Time Entries
     *          Sortie Report - Listing of the sorties on the mission
     * 
     * New Development 01/23/2019 
     * Feature - Sortie Notes.  Allow the user to add notes to a sortie 15 hrs
     * Feature - Mission Catalog.  Allows for the entry of multiple misions in the app.  15 hrs
     * 
     * 
     * Sortie Summary Page - Clicking on the sortie row, selecting the sortie will call the Sortie Management page.
     * 
     * 
     * FlexLayout - Needs to be investigated
     * 
     */
}
