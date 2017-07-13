using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GradientPicker.UI.UserControl
{
    public class Swatches : Control
    {
        static Swatches()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Swatches), new FrameworkPropertyMetadata(typeof(Swatches)));
        }

        ObservableCollection<SolidColorBrush> _lsBrush;
        public ObservableCollection<SolidColorBrush> lsBrush
        {
            get { return _lsBrush; }
            set { _lsBrush = value; }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InitialColor();
            
        }

        void InitialColor()
        {
            lsBrush = new ObservableCollection<SolidColorBrush>();
            ls = new ObservableCollection<Color>();
            ls.Add(Color.FromRgb(255, 0, 0));
            ls.Add(Color.FromRgb(255, 127, 0));
            ls.Add(Color.FromRgb(255, 255, 0));
            ls.Add(Color.FromRgb(0, 255, 0));
            ls.Add(Color.FromRgb(0, 0, 255));
            ls.Add(Color.FromRgb(75, 0, 130));
            ls.Add(Color.FromRgb(139, 0, 255));
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 0));


            for (int i = 0; i < ls.Count; i++)
            {
                SolidColorBrush b = new SolidColorBrush();
                b.Color = ls[i];
                lsBrush.Add(b);
            }
            _mColor = new Color();
            _mColor = Color.FromRgb(0, 0, 0);
        }
        
        public ObservableCollection<Color> ls
        {
            get { return (ObservableCollection<Color>)GetValue(lsProperty); }
            set { SetValue(lsProperty,value); }
        }
        public static readonly DependencyProperty lsProperty = DependencyProperty.Register("ls", typeof(ObservableCollection<Color>), typeof(Swatches));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedBrushProperty); }
            set
            {
                SetValue(SelectedBrushProperty, _mColor);
                setColor((Color)value);
            }
        }

        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(Swatches));

        Color _mColor;
        private void setColor(Color theColor)
        {
            _mColor = theColor;
        }

        private static void onDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int a = 0;
        }
    }
}
