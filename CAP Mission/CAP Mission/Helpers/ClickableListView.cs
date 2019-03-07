using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CAPMission.Helpers
{
    public class ClickableListView : ListView
    {
        public static BindableProperty RowClickCommandProperty = BindableProperty.Create(nameof(RowClickCommand), typeof(ICommand), typeof(ClickableListView), null);
        public ICommand RowClickCommand
        {
            get
            {
                return (ICommand)this.GetValue(RowClickCommandProperty);
            }
            set
            {
                this.SetValue(RowClickCommandProperty, value);
            }
        }

        public ClickableListView()
        {
            this.ItemSelected += ClickableListView_ItemSelected;
        }

        private void ClickableListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           if (e.SelectedItem != null)
                {
                RowClickCommand?.Execute(e.SelectedItem);
                SelectedItem = null;
            }
        }
    }
}
