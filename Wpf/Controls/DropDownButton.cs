using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Wanderer.Library.Wpf.Controls
{
    /// <summary>
    /// Extended <see cref="ToggleButton"/> with drop-down menu.
    /// </summary>
    public class DropDownButton : ToggleButton
    {
        /// <summary>
        /// Describes the position of drop-down menu.
        /// </summary>
        public enum Placement
        {
            /// <summary>
            /// Drop-down menu appears at the bottom of button.
            /// </summary>
            Bottom,
            /// <summary>
            /// Drop-down menu appears from the right side of the button.
            /// </summary>
            Right
        }

        /// <summary>
        /// Describes the position of drop-down menu.
        /// </summary>
        public Placement DropDownPlacement { private get; set; }

        #region DropDown (DependencyProperty)
        /// <summary>
        /// The <see cref="ContextMenu"/> object for drop-down menu.
        /// </summary>
        public ContextMenu DropDown
        {
            get { return (ContextMenu) GetValue(DropDownProperty); }
            set { SetValue(DropDownProperty, value); }
        }

        /// <summary>
        /// Attached property for <see cref="DropDown"/>.
        /// </summary>
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