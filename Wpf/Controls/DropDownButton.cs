using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Wanderer.Library.Wpf.Controls
{
    public class DropDownButton : ToggleButton
    {
        public enum Placement
        {
            Bottom,
            Right
        }

        public Placement DropDownPlacement { private get; set; }

        #region DropDown (DependencyProperty)
        public ContextMenu DropDown
        {
            get { return (ContextMenu) GetValue(DropDownProperty); }
            set { SetValue(DropDownProperty, value); }
        }

        public static readonly DependencyProperty DropDownProperty =
            DependencyProperty.Register("DropDown", typeof (ContextMenu), typeof (DropDownButton),
                                        new PropertyMetadata(null, OnDropDownChanged));

        private static void OnDropDownChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((DropDownButton) sender).OnDropDownChanged(e);
        }

        private void OnDropDownChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DropDown != null)
            {
                DropDown.PlacementTarget = this;

                switch (DropDownPlacement)
                {
                    default:
                    case Placement.Bottom:
                        DropDown.Placement = PlacementMode.Bottom;
                        break;
                    case Placement.Right:
                        DropDown.Placement = PlacementMode.Right;
                        break;
                }

                this.Checked += (a, b) => { DropDown.IsOpen = true; };
                this.Unchecked += (a, b) => { DropDown.IsOpen = false; };
                DropDown.Closed += (a, b) => { this.IsChecked = false; };
            }
        }
        #endregion
    }
}