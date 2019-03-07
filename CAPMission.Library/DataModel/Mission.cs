using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CAPMission.Library.DataModel
{
    public class Mission
    {
        public string MissionNumber { get; set; }
        public ObservableCollection<Sortie> Sorties { get; set; }
        public ObservableCollection<SortieNote> SortieNotes { get; set; }
        public Mission()
        {
            Sorties = new ObservableCollection<Sortie>();
            SortieNotes = new ObservableCollection<SortieNote>();
        }
    }
    public class Sortie
    {
        public string Number { get; set; }
        public string CAPFlight { get; set; }
        public string Tail { get; set; }
        public double EndHobbs { get; set; }
        public double StartHobbs { get; set; }
        public double EndTach { get; set; }
        public double StartTach { get; set; }
        public DateTime SortieDate { get; set; }
        public DateTime EngineStart { get; set; }
        public DateTime WheelsUp { get; set; }
        public DateTime InGrid { get; set; }
        public DateTime OutGrid { get; set; }
        public DateTime WheelsDown { get; set; }
        public DateTime EngineStop { get; set; }
        public string TachTime
        {
            get
            {
                return (EndTach - StartTach).ToString("N1");
            }
        }
        public string HobbsTime
        {
            get
            {
                return (EndHobbs - StartHobbs).ToString("N1");
            }
        }
        public Sortie()
        {
            SortieDate = DateTime.Now;
        }
    }

    public class GPSConvertedValue
    {
        public string LatDegDM { get; set; }
        public string LongDegDM { get; set; }
        public string LatDecDegrees { get; set; }
        public string LongDecDegrees { get; set; }
    }
    public class MarkPoint
    {
        public string Name { get; set; }
        public double Latitiude { get; set; }
        public double Longitude { get; set; }
        public string LatDegDM { get; set; }
        public string LongDegDM { get; set; }
        public string MarkNote { get; set; }
    }
    public class SortieNote
    {
        public string Number { get; set; }
        public string Tasking { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public ObservableCollection<MarkPoint> Marks { get; set; }
        public SortieNote()
        {
            Marks = new ObservableCollection<MarkPoint>();
        }
    }
    public class UserSettings
    {
        public List<string> TailNumbers { get; set; }
    }
    public class ListSortie
    {
        public string Number { get; set; }
        public string Tail { get; set; }
        public DateTime SortieDate { get; set; }
        public bool Notes { get; set; }
    }
}
