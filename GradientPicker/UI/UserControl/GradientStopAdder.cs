using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GradientPicker.UI.UserControl
{
    class GradientStopAdder:Button
    {
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (e.Source is GradientStopAdder && this.gradientPicker != null)
            {
                Button btn = e.Source as Button;

                GradientStop _gs = new GradientStop();
                _gs.Offset = Mouse.GetPosition(btn).X / btn.ActualWidth;
                _gs.Color = this.gradientPicker.Color;
                //_gs.Color = GetColorFromImage(e.GetPosition(this));
                this.gradientPicker.Gradients.Add(_gs);
                this.gradientPicker.SelectedGradient = _gs;
                this.gradientPicker.Color = _gs.Color;
                this.gradientPicker.SetBrush();
            }
        }

        //Color GetColorFromImage(Point p)
        //{
        //    try
        //    {
        //    }
        //    catch (Exception)
        //    {
        //        return
        //    }
        //}

        public GradientPickerControl gradientPicker
        {
            get { return (GradientPickerControl)GetValue(GradientPickerProperty); }
            set { SetValue(GradientPickerProperty, value); }
        }

        public static readonly DependencyProperty GradientPickerProperty = DependencyProperty.Register("gradientPicker", typeof(GradientPickerControl), typeof(GradientStopAdder));
    }
}
