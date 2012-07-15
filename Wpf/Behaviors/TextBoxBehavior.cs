using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Wanderer.Library.Extensions;

namespace Wanderer.Library.Wpf.Behaviors
{
    /// <summary>
    /// <see cref="TextBox"/> attached properties with some special behavior.
    /// </summary>
    public static class TextBoxBehavior
    {
        #region IsInt32 behavior
        /// <summary>
        /// Attached property that restricts <see cref="TextBox"/> input only <see cref="System.Int32"/> numbers.
        /// </summary>
        /// <remarks>
        /// This attached property works only for <see cref="TextBox"/> or for its descendants.
        /// </remarks>
        public static readonly DependencyProperty IsInt32Property =
            DependencyProperty.RegisterAttached("IsInt32", typeof (bool), typeof (TextBoxBehavior),
                                                new FrameworkPropertyMetadata(false, OnIsInt32Changed));

        /// <summary>
        /// Gets value for <see cref="IsInt32Property"/> property.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof (TextBox))]
        public static bool GetIsInt32(DependencyObject element)
        {
            return (bool) element.GetValue(IsInt32Property);
        }

        /// <summary>
        /// Sets value for <see cref="IsInt32Property"/> property.
        /// </summary>
        public static void SetIsInt32(DependencyObject element, bool value)
        {
            element.SetValue(IsInt32Property, value);
        }

        private static void OnIsInt32Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (TextBox) d;

            textBox.PreviewTextInput += PreviewTextInputForInt32;
            DataObject.AddPastingHandler(textBox, OnPasteHandlerForInt32);
            // Fix coerce a textbox in .net 4.0
            // http://stackoverflow.com/questions/3905227/coerce-a-wpf-textbox-not-working-anymore-in-net-4-0
            textBox.TextChanged += TextBoxTextChanged;
        }

        private static void OnPasteHandlerForInt32(object sender, DataObjectPastingEventArgs e)
        {
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.Text, true);
            if (!isText) return;

            var clipboardText = (string) e.SourceDataObject.GetData(DataFormats.Text);
            var text = GetFullText((TextBox) sender, clipboardText);

            if (!text.IsInt32())
                e.CancelCommand();
        }

        private static void PreviewTextInputForInt32(object sender, TextCompositionEventArgs e)
        {
            var text = GetFullText((TextBox) sender, e.Text);

            e.Handled = !text.IsInt32();
        }
        #endregion

        #region IsUInt32 behavior
        /// <summary>
        /// Attached property that restricts <see cref="TextBox"/> input only <see cref="System.UInt32"/> numbers.
        /// </summary>
        /// <remarks>
        /// This attached property works only for <see cref="TextBox"/> or for its descendants.
        /// </remarks>
        public static readonly DependencyProperty IsUInt32Property =
            DependencyProperty.RegisterAttached("IsUInt32", typeof (bool), typeof (TextBoxBehavior),
                                                new FrameworkPropertyMetadata(false, OnIsUInt32Changed));

        /// <summary>
        /// Gets value for <see cref="IsUInt32Property"/> property.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof (TextBox))]
        public static bool GetIsUInt32(DependencyObject element)
        {
            return (bool) element.GetValue(IsUInt32Property);
        }

        /// <summary>
        /// Sets value for <see cref="IsUInt32Property"/> property.
        /// </summary>
        public static void SetIsUInt32(DependencyObject element, bool value)
        {
            element.SetValue(IsUInt32Property, value);
        }

        private static void OnIsUInt32Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (TextBox) d;

            textBox.PreviewTextInput += PreviewTextInputForUInt32;
            DataObject.AddPastingHandler(textBox, OnPasteHandlerForUInt32);
            // Fix coerce a textbox in .net 4.0
            // http://stackoverflow.com/questions/3905227/coerce-a-wpf-textbox-not-working-anymore-in-net-4-0
            textBox.TextChanged += TextBoxTextChanged;
        }

        private static void OnPasteHandlerForUInt32(object sender, DataObjectPastingEventArgs e)
        {
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.Text, true);
            if (!isText) return;

            var clipboardText = (string) e.SourceDataObject.GetData(DataFormats.Text);
            var text = GetFullText((TextBox) sender, clipboardText);

            if (!text.IsUInt32())
                e.CancelCommand();
        }

        private static void PreviewTextInputForUInt32(object sender, TextCompositionEventArgs e)
        {
            var text = GetFullText((TextBox) sender, e.Text);

            e.Handled = !text.IsUInt32();
        }
        #endregion

        #region IsDecimal behavior
        /// <summary>
        /// Attached property that restricts <see cref="TextBox"/> input only <see cref="System.Decimal"/> numbers.
        /// </summary>
        /// <remarks>
        /// This attached property works only for <see cref="TextBox"/> or for its descendants.
        /// </remarks>
        public static readonly DependencyProperty IsDecimalProperty =
            DependencyProperty.RegisterAttached("IsDecimal", typeof (bool), typeof (TextBoxBehavior),
                                                new FrameworkPropertyMetadata(false, OnIsDecimalChanged));

        /// <summary>
        /// Gets value for <see cref="IsDecimalProperty"/> property.
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof (TextBox))]
        public static bool GetIsDecimal(DependencyObject element)
        {
            return (bool) element.GetValue(IsDecimalProperty);
        }

        /// <summary>
        /// Sets value for <see cref="IsDecimalProperty"/> property.
        /// </summary>
        public static void SetIsDecimal(DependencyObject element, bool value)
        {
            element.SetValue(IsDecimalProperty, value);
        }

        private static void OnIsDecimalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (TextBox) d;

            textBox.PreviewTextInput += PreviewTextInputForDecimal;
            DataObject.AddPastingHandler(textBox, OnPasteHandlerForDecimal);
            // Fix coerce a textbox in .net 4.0
            // http://stackoverflow.com/questions/3905227/coerce-a-wpf-textbox-not-working-anymore-in-net-4-0
            textBox.TextChanged += TextBoxTextChanged;
        }

        private static void OnPasteHandlerForDecimal(object sender, DataObjectPastingEventArgs e)
        {
            var isText = e.SourceDataObject.GetDataPresent(DataFormats.Text, true);
            if (!isText) return;

            var clipboardText = (string) e.SourceDataObject.GetData(DataFormats.Text);
            var text = GetFullText((TextBox) sender, clipboardText);

            if (!text.IsDecimal())
                e.CancelCommand();
        }

        private static void PreviewTextInputForDecimal(object sender, TextCompositionEventArgs e)
        {
            var text = GetFullText((TextBox) sender, e.Text);

            e.Handled = !text.IsDecimal();
        }
        #endregion

        private static void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
            {
                var expression = textBox.GetBindingExpression(TextBox.TextProperty);

                if (expression != null)
                    expression.UpdateSource();
            }
        }

        private static string GetFullText(TextBox textBox, string inputText)
        {
            return textBox.SelectionLength > 0
                       ? textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength).Insert(textBox.SelectionStart, inputText)
                       : textBox.Text.Insert(textBox.CaretIndex, inputText);
        }
    }
}