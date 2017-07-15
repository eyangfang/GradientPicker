using System.Collections.ObjectModel;
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
            m_ColorList = GetTemplateChild(ColorListName) as ListBox;
            m_ColorList.SelectionChanged += new SelectionChangedEventHandler(SetColor);
        }

        void InitialColor()
        {
            lsBrush = new ObservableCollection<SolidColorBrush>();
            ls = new ObservableCollection<Color>();
            ls.Add(Color.FromArgb(255, 255, 0, 0));
            ls.Add(Color.FromArgb(255, 255, 127, 0));
            ls.Add(Color.FromArgb(255, 255, 255, 0));
            ls.Add(Color.FromArgb(255, 0, 255, 0));
            ls.Add(Color.FromArgb(255, 0, 0, 255));
            ls.Add(Color.FromArgb(255, 75, 0, 130));
            ls.Add(Color.FromArgb(255, 139, 0, 255));
            ls.Add(Color.FromArgb(0, 0, 0, 0));
            ls.Add(Color.FromArgb(0, 0, 0, 0));
            ls.Add(Color.FromArgb(0, 0, 0, 0));
            ls.Add(Color.FromArgb(0, 0, 0, 0));
            ls.Add(Color.FromArgb(0, 0, 0, 0));
            ls.Add(Color.FromArgb(0, 0, 0, 0));
            ls.Add(Color.FromArgb(0, 0, 0, 0));


            for (int i = 0; i < ls.Count; i++)
            {
                SolidColorBrush b = new SolidColorBrush();
                b.Color = ls[i];
                lsBrush.Add(b);
            }
        }
        
        public ObservableCollection<Color> ls
        {
            get { return (ObservableCollection<Color>)GetValue(lsProperty); }
            set { SetValue(lsProperty,value); }
        }
        public static readonly DependencyProperty lsProperty = DependencyProperty.Register("ls", typeof(ObservableCollection<Color>), typeof(Swatches));

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(Swatches), new PropertyMetadata(Colors.Black, OnSelectedColorChanged));

        public void SetColor(object sender, SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0)
            {
                string s = args.AddedItems[0].ToString();
                m_SelectedColor = (Color)ColorConverter.ConvertFromString(s);
                SelectedColor = m_SelectedColor;
            }
        }

        public static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            Swatches c = (Swatches)o;
            if (e.NewValue is Color)
            {
                c.SelectedColor = (Color)e.NewValue;
                if (c.ls!=null &&!c.ls[0].Equals(c.SelectedColor))
                {
                    c.m_SelectedColor = c.SelectedColor;
                    c.updateColorList();
                }
            }
        }

        internal void updateColorList()
        {
            Color newColor = m_SelectedColor;
            if (!ls.Contains(newColor))
            {
                ls.RemoveAt(ls.Count - 1);
                ls.Insert(0, newColor);
            }
            //else
            //{
            //    int idx = ls.IndexOf(newColor);
            //    ls.Move(idx,0);
            //}
        }

        private ListBox m_ColorList;
        private static readonly string ColorListName = "Part_ColorList";
        public Color m_SelectedColor;
    }
}
