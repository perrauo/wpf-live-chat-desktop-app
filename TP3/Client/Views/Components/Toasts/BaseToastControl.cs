using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IFT585_TP3.Client
{
    /// <summary>
    /// Interaction logic for ToastControl.xaml
    /// </summary>
    public class BaseToastControl : UserControl
    {
        public Action<BaseToastControl> OnRemovedHandler { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChangeVisualState();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BaseToastControl));

        public static readonly DependencyProperty IsToastVisibleProperty =
            DependencyProperty.Register("IsToastVisible", typeof(bool), typeof(BaseToastControl), new PropertyMetadata(false, OnIsToastVisibleChanged));

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(BaseToastControl), new PropertyMetadata(TimeSpan.FromSeconds(10), OnDurationChanged));

        public static readonly DependencyProperty ImageGeometryProperty =
            DependencyProperty.Register("ImageGeometry", typeof(Geometry), typeof(BaseToastControl));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public void Animate()
        {
            SetValue(IsToastVisibleProperty, true);
            ChangeVisualState();
        }

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public Geometry ImageGeometry
        {
            get { return (Geometry)GetValue(ImageGeometryProperty); }
            set { SetValue(ImageGeometryProperty, value); }
        }

        private static void OnDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (BaseToastControl)d;
            var value = (TimeSpan)e.NewValue;
            control.Duration = value;
        }

        private static void OnIsToastVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (BaseToastControl)d;
            var value = (bool)e.NewValue;
        }

        private void ChangeVisualState()
        {

            Opacity = 1;

            DoubleAnimation da = new DoubleAnimation { From = 64, To = -16, Duration = TimeSpan.FromSeconds(.5f) };

            CubicEase cubicEase = new CubicEase();
            cubicEase.EasingMode = EasingMode.EaseInOut;

            da.EasingFunction = cubicEase;
            
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, da);
        }

        public enum ToastIconType
        {
            None = 0,
            Information = 1,
            Warning = 2
        }
    }
}
