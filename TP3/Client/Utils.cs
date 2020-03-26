using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Client
{
    public enum Status
    {
        ConnectionError,
        NonExistentItem,
        Success
    }

    public class Result<T>
    {
        public Status Status { get; set; }
        public T Return { get; set; } 
    }

    public static class Utils
    {
        internal static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }

        public static void SetEnabled(this System.Windows.UIElement elem, bool value)
        {
            elem.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            elem.IsEnabled = value;           
        }
    }
}
