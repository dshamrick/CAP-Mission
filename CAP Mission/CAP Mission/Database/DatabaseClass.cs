using CAPMission.Interfaces;
using CAPMission.Library.DataModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CAPMission.Database
{
    public class DatabaseClass
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        private ObservableCollection<Aircraft> aircraftList;

        public ObservableCollection<Aircraft> AircraftList
        {
            get
            { if (aircraftList == null)
                    aircraftList = new ObservableCollection<Aircraft>(database.Table<Aircraft>());
                return aircraftList;
            }
        }

        public DatabaseClass()
        {
            database =
            DependencyService.Get<IDatabase>().
            DbConnection();
            database.CreateTable<Aircraft>();
        }
    }
}
