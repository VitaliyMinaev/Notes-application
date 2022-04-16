using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Notes.CustomItems;
using Notes.Droid.Custom_items;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace Notes.Droid.Custom_items
{
    enum CheckStates
    {
        Selected = 0,
        Unselected = 1
    }

    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private CheckStates checkStates = CheckStates.Selected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _unselectedBackground = _cellCore.Background;
            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            // To except notification that has been executed from changing mode
            if (args.PropertyName != "IsSelected")
                return;

            base.OnCellPropertyChanged(sender, args);

            if (checkStates == CheckStates.Selected)
            {
                var extendedViewCell = sender as CustomViewCell;
                _cellCore.SetBackgroundColor(extendedViewCell.SelectedItemBackgroundColor.ToAndroid());

                checkStates = CheckStates.Unselected;
            }
            else if (checkStates == CheckStates.Unselected)
            {
                _cellCore.SetBackground(_unselectedBackground);

                checkStates = CheckStates.Selected;
            }
        }
    }
}