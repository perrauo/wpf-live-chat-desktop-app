using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace IFT585_TP3.Client
{
    public static class Utils
    {
        internal static IEnumerable<T> FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
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

            return results;
        }

        public static void SetEnabled(this System.Windows.UIElement elem, bool value)
        {
            if (elem == null) return;

            elem.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            elem.IsEnabled = value;
        }

        public const string ValidUsernamePattern = "^[[A-Z]|[a-z]][[A-Z]|[a-z]|\\d|[_]]{7,29}$";
        public static bool IsValidUserName(string username) => Regex.IsMatch(username, ValidUsernamePattern);

        public const string ValidUserPasswordPattern = "^[[A-Z]|[a-z]][[A-Z]|[a-z]|\\d|[_]]{7,29}$";
        public static bool IsValidUserPassword(string pass) => Regex.IsMatch(pass, ValidUserPasswordPattern);

    }

}
