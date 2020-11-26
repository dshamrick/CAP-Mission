using System;
using System.IO;
using CAPMission.Interfaces;
using CAPMission.iOS;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseService))]
namespace CAPMission.iOS
{
    public class DatabaseService : IDatabase
    {
        SQLiteConnection IDatabase.DbConnection()
        {
            var dbName = "MissionDb.db3";
            string personalFolder =
              System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =
              Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}