using System;
using CAPMission.Interfaces;
using CAPMission.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SendFlightAlertService))]
namespace CAPMission.iOS
{
    public class SendFlightAlertService : ISendFlightAlert
    {
        public void SendFlightAlert(string AlertMessage)
        {
            //UIAlertController alert = UIAlertController.Create("Inflight Alert",AlertMessage,UIAlertControllerStyle.Alert);
            //alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
            //UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, animated: true, completionHandler: null);
            new UIAlertView("Inflight Alert", AlertMessage, null, "OK", null).Show();
        }
    }
}