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
    public class ToastControl : UserControl
    {
        public Action<ToastControl> OnRemovedHandler { get; set; }

        static ToastControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastControl), new FrameworkPropertyMetadata(typeof(ToastControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChangeVisualState();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ToastControl), new PropertyMetadata("Sample Text"));
       
        public static readonly DependencyProperty IsToastVisibleProperty =
            DependencyProperty.Register("IsToastVisible", typeof(bool), typeof(ToastControl), new PropertyMetadata(false, OnIsToastVisibleChanged));

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(ToastControl), new PropertyMetadata(TimeSpan.FromSeconds(10), OnDurationChanged));

        public static readonly DependencyProperty ImageGeometryProperty =
            DependencyProperty.Register("ImageGeometry", typeof(Geometry), typeof(ToastControl));

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
            var control = (ToastControl)d;
            var value = (TimeSpan)e.NewValue;
            control.Duration = value;
        }

        private static void OnIsToastVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (ToastControl)d;
            var value = (bool)e.NewValue;
            //c.IsToastVisible = value;
        }

        private void ChangeVisualState()
        {
            
            Opacity = 1;

            DoubleAnimation da = new DoubleAnimation { From = 64, To = 0, Duration = TimeSpan.FromSeconds(.5f) };

            CubicEase cubicEase = new CubicEase();
            cubicEase.EasingMode = EasingMode.EaseInOut;

            da.EasingFunction = cubicEase;

            //da.Completed += (sender, e) => IsToastVisible = false;
            RenderTransform.BeginAnimation(TranslateTransform.YProperty, da);            
        }

        public enum ToastIconType
        {
            None = 0,
            Information = 1,
            Warning = 2
        }
    }

    public class GroupRequestToastControl : ToastControl
    {
        static GroupRequestToastControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupRequestToastControl), new FrameworkPropertyMetadata(typeof(GroupRequestToastControl)));
        }
    }

    public class AdminRequestToastControl : ToastControl
    {
        static AdminRequestToastControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdminRequestToastControl), new FrameworkPropertyMetadata(typeof(AdminRequestToastControl)));
        }
    }

    public class SuccessToastControl : ToastControl
    {
        static SuccessToastControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SuccessToastControl), new FrameworkPropertyMetadata(typeof(SuccessToastControl)));
        }
    }

    public class ErrorToastControl : ToastControl
    {
        static ErrorToastControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ErrorToastControl), new FrameworkPropertyMetadata(typeof(ErrorToastControl)));
        }

        //When login button is pressed, it tries to login user
        private void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            OnRemovedHandler?.Invoke(this);
        }
    }
}
