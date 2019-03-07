using System;
using System.Collections.Generic;
using System.Text;

namespace CAPMission.Library
{
    public static class GPSConvert
    {

        public static string ConvertToDMS(string decimalDegrees)
        {
            string retVal = string.Empty;
            string remainder;
            string[] degrees = new string[2];
            string lDeg;
            float multiplier;
            try
            {
                degrees = decimalDegrees.Split('.');
                lDeg = degrees[0];
                remainder = "0." + degrees[1];
                multiplier = float.Parse(remainder) * 60;
                retVal = lDeg + "° " + multiplier.ToString();
            }
            catch (Exception ex)
            {
                retVal = "ERROR";
            }

            return retVal;
        }
    }
}
