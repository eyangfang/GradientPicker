using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            ls = new List<Color>();
            ls.Add(Color.FromRgb(0, 0, 0));
            ls.Add(Color.FromRgb(0, 0, 255));
            ls.Add(Color.FromRgb(165, 42, 42));
            ls.Add(Color.FromRgb(127, 255, 0));
            ls.Add(Color.FromRgb(0, 255, 255));
            ls.Add(Color.FromRgb(255, 0, 0));
            ls.Add(Color.FromRgb(255, 255, 255));
            ls.Add(Color.FromRgb(205, 92, 92));
            ls.Add(Color.FromRgb(255, 255, 0));
            ls.Add(Color.FromRgb(154, 205, 50));
            ls.Add(Color.FromRgb(255, 160, 122));
            ls.Add(Color.FromRgb(255, 0, 255));
            ls.Add(Color.FromRgb(186, 85, 211));
            ls.Add(Color.FromRgb(123, 104, 238));

            
            for (int i = 0; i < ls.Count; i++)
            {
                SolidColorBrush b = new SolidColorBrush();
                b.Color = ls[i];
                lsBrush.Add(b);
            }
            
        }
        
        public List<Color> ls
        {
            get { return (List<Color>)GetValue(lsProperty); }
            set { SetValue(lsProperty,value); }
        }
        public static readonly DependencyProperty lsProperty = DependencyProperty.Register("ls", typeof(List<Color>), typeof(Swatches));

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            Button btn = e.Source as Button;
        }


    }
}
