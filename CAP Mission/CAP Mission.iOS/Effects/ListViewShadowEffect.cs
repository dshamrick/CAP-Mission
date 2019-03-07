using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using CAPMission.iOS.Effects;
using System.ComponentModel;

[assembly: ResolutionGroupName("com.hssi")]
[assembly: ExportEffect(typeof(ListViewShadowEffect),"ListViewShadowEffect")]
namespace CAPMission.iOS.Effects
{
    public class ListViewShadowEffect : PlatformEffect<ListView, ListView>
    {
        protected override void OnAttached()
        {
            var c = Control as ListView;
        }

        protected override void OnDetached()
        {
            throw new NotImplementedException();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }
    }
}