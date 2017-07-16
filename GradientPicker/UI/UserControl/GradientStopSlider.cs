using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GradientPicker.UI.UserControl
{
    class GradientStopSlider:Slider
    {
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (this.gradientPicker != null)
            {
                this.gradientPicker._BrushSetInternally = true;
                this.gradientPicker._UpdateBrush = false;

                this.gradientPicker.SelectedGradient = this.SelectedGradient;
                this.gradientPicker.Color = this.SelectedGradient.Color;

                this.gradientPicker._UpdateBrush = true;
            }
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            if (this.gradientPicker != null)
            {
                this.gradientPicker._BrushSetInternally = true;
                this.gradientPicker.SetBrush();
            }
        }

        public GradientPickerControl gradientPicker
        {
            get { return (GradientPickerControl)GetValue(GradientPickerProperty); }
            set { SetValue(GradientPickerProperty, value); }
        }
        public static readonly DependencyProperty GradientPickerProperty =
            DependencyProperty.Register("gradientPicker", typeof(GradientPickerControl), typeof(GradientStopSlider));

        public GradientStop SelectedGradient
        {
            get { return (GradientStop)GetValue(SelectedGradientProperty); }
            set { SetValue(SelectedGradientProperty, value); }
        }
        public static readonly DependencyProperty SelectedGradientProperty =
            DependencyProperty.Register("SelectedGradient", typeof(GradientStop), typeof(GradientStopSlider));
    }

    
}
