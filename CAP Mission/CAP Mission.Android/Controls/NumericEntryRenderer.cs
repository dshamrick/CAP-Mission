using Android.Content;
using CAPMission.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NumericEntry), typeof(CAPMission.Droid.Controls.NumericEntryRenderer))]
namespace CAPMission.Droid.Controls
{
    public class NumericEntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public NumericEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {   // perform initial setup
                // lets get a reference to the native control
                var nativeEditText = (global::Android.Widget.EditText)Control;
                NumericEntry numericEntry = e.NewElement as NumericEntry;
                if (numericEntry.MaxLength > 0)
                {
                    nativeEditText.SetFilters(new global::Android.Text.IInputFilter[] { new global::Android.Text.InputFilterLengthFilter(numericEntry.MaxLength) });
                }
                // do whatever you want to the textField here!
                nativeEditText.SetRawInputType(global::Android.Text.InputTypes.ClassNumber);
            }
        }
    }
}