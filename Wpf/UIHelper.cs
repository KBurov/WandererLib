using System.Windows;
using System.Windows.Media;

namespace Wanderer.Library.Wpf
{
    /// <summary>
    /// Class with helpful methods for various UI needs.
    /// </summary>
// ReSharper disable once InconsistentNaming
    public static class UIHelper
    {
        /// <summary>
        /// Finds a child of a given item in the visual tree.
        /// </summary>
        /// <typeparam name="T">a type of the queried item</typeparam>
        /// <param name="parent">a direct parent of the queried item</param>
        /// <param name="childName">x:Name or Name of child</param>
        /// <returns>
        /// The first child item that matches the submited type parameter.
        /// If not matching item can be found, a null parent is being returned.
        /// </returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;
            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0;i < childrenCount;i++) {
                var child = VisualTreeHelper.GetChild(parent, i);
                var typedChild = child as T;

                if (typedChild == null) {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName)) {
                    var frameworkElement = child as FrameworkElement;

                    if (frameworkElement != null && frameworkElement.Name == childName) {
                        foundChild = typedChild;
                        break;
                    }
                }
                else {
                    foundChild = typedChild;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// Finds a parent a given item in the visual tree.
        /// </summary>
        /// <typeparam name="T">a type of the queried item</typeparam>
        /// <param name="child">a direct or indirect child of the queried item</param>
        /// <returns>
        /// The first parent item that matches the submited type parameter.
        /// If not matching item can be found, a null parent is being returned.
        /// </returns>
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            var parent = parentObject as T;

            return parent ?? FindParent<T>(parentObject);
        }
    }
}