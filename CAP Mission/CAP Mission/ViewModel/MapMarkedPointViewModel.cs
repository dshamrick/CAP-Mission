using CAPMission.Library.DataModel;
using System;
using System.Collections.Generic;
using System.Text;
//using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CAPMission.ViewModel
{
    public class MapMarkedPointViewModel : ViewModelBase
    {
        private SortieNote displayNote;
        private MarkPoint displayPoint;
        public Map PointMap { get; set; }
        public MapMarkedPointViewModel(INavigation navigation, MarkPoint note) : base(navigation)
        {
            displayPoint = note;
            DIsplayPointsOnMap();
        }

        private void DIsplayPointsOnMap()
        {
            //using Xamarin.Essentials;
            //Location loc = new Location();
            //loc.Latitude = showPoint.Latitiude;
            //loc.Longitude = showPoint.Longitude;
            //var placemark = new Placemark();
            //await Map.OpenAsync(loc);
            PointMap = new Map();
            Position posit1 = new Position(displayPoint.Latitiude, displayPoint.Longitude);
            Pin point1 = new Pin();
            point1.Position = posit1;
            point1.Type = PinType.Place;
            point1.Label = displayPoint.Name;
            PointMap.Pins.Add(point1);
            RaisePropertyChanged(nameof(PointMap));
            //foreach (MarkPoint point in displayNote.Marks)
            //{
            //    Position posit1 = new Position(point.Latitiude, point.Longitude);
            //    Pin point1 = new Pin();
            //    point1.Position = posit1;
            //    point1.Type = PinType.Place;
            //    point1.Label = point.Name;
            //    PointMap.Pins.Add(point1);
            //}
        }
    }
}
