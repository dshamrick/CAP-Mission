using CAPMission.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumericEntry), typeof(CAPMission.iOS.Controls.NumericEntryRenderer))]
namespace CAPMission.iOS.Controls
{    
    public class NumericEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {   // perform initial setup
                // lets get a reference to the native control
                var nativeEditor = (UITextField)Control;
                NumericEntry numericEntry = e.NewElement as NumericEntry;
                if (numericEntry.MaxLength > 0)
                {
                    nativeEditor.ShouldChangeCharacters = (textField, range, replacementString) =>
                    {
                        var newLength = textField.Text.Length + replacementString.Length - range.Length;
                        return newLength <= numericEntry.MaxLength;
                    };
                }
                // do whatever you want to the textField here!
                nativeEditor.KeyboardType = UIKeyboardType.NumberPad;
            }
        }
    }
}
