using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CAPMission.Controls
{
    public class NumericEntry : Xamarin.Forms.Entry
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof(int), typeof(NumericEntry), -1, BindingMode.OneWay, null, null, null, null);

        public int MaxLength
        {
            get
            {
                return (int)base.GetValue(NumericEntry.MaxLengthProperty);
            }
            set
            {
                base.SetValue(NumericEntry.MaxLengthProperty, value);
            }
        }
    }
}
