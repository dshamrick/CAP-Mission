using System;
using System.Collections.Generic;
using System.Text;

namespace CAPMission.Interfaces
{
    public interface IDatabase
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
