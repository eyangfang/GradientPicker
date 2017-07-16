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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InitialColor();
            m_ColorList = GetTemplateChild(ColorListName) as ListBox;
            m_ColorList.SelectionChanged += new SelectionChangedEventHandler(SetColor);
        }

        void InitialColor()
        {
            listColor = new ObservableCollection<Color>();
            listColor.Add(Color.FromArgb(255, 255, 0, 0));
            listColor.Add(Color.FromArgb(255, 255, 127, 0));
            listColor.Add(Color.FromArgb(255, 255, 255, 0));
            listColor.Add(Color.FromArgb(255, 0, 255, 0));
            listColor.Add(Color.FromArgb(255, 0, 0, 255));
            listColor.Add(Color.FromArgb(255, 75, 0, 130));
            listColor.Add(Color.FromArgb(255, 139, 0, 255));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
            listColor.Add(Color.FromArgb(0, 0, 0, 0));
        }
        
        public ObservableCollection<Color> listColor
        {
            get { return (ObservableCollection<Color>)GetValue(lsProperty); }
            set { SetValue(lsProperty,value); }
        }
        public static readonly DependencyProperty lsProperty = DependencyProperty.Register("listColor", typeof(ObservableCollection<Color>), typeof(Swatches));

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
                if (c.listColor != null &&!c.listColor[0].Equals(c.SelectedColor))
                {
                    c.m_SelectedColor = c.SelectedColor;
                    c.updateColorList();
                }
            }
        }

        internal void updateColorList()
        {
            Color newColor = m_SelectedColor;
            if (!listColor.Contains(newColor))
            {
                listColor.RemoveAt(listColor.Count - 1);
                listColor.Insert(0, newColor);
            }
        }

        private ListBox m_ColorList;
        private static readonly string ColorListName = "Part_ColorList";
        public Color m_SelectedColor;
    }
}
