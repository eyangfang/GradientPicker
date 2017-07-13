using System.Windows;
using System.Windows.Media;

namespace GradientPicker.UI.UserControl
{
    /// <summary>
    /// Interaction logic for ColorPickerDialog.xaml
    /// </summary>
    public partial class ColorPickerDialog : Window
    {
        public ColorPickerDialog()
        {
            InitializeComponent();
        }

        public void SetLocString(string[] strArray)
        {
            if (strArray.Length < 16) return;

            Title = strArray[0];
            OKButton.Content = strArray[1];
            btnCancel.Content = strArray[2];

            cPicker.txtSlectedColor = strArray[3];
            cPicker.txtOpacity = strArray[4];
            cPicker.txtHexNotation = strArray[5];

            cPicker.txtScRGB = strArray[6];
            cPicker.txtsRGB = strArray[7];
            cPicker.txtScA = strArray[8];
            cPicker.txtScR = strArray[9];
            cPicker.txtScG = strArray[10];
            cPicker.txtScB = strArray[11];
            cPicker.txtColorA = strArray[12];
            cPicker.txtColorR = strArray[13];
            cPicker.txtColorG = strArray[14];
            cPicker.txtColorB = strArray[15];
        }
        //

        private void okButtonClicked(object sender, RoutedEventArgs e)
        {

            OKButton.IsEnabled = false;
            m_color = cPicker.SelectedColor;
            A_color = cPicker.A;
            R_color = cPicker.R;
            G_color = cPicker.G;
            B_color = cPicker.B;
            DialogResult = true;
            Hide();

        }


        private void cancelButtonClicked(object sender, RoutedEventArgs e)
        {

            OKButton.IsEnabled = false;
            DialogResult = false;

        }

        private void onSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

            if (e.NewValue != m_color)
            {

                OKButton.IsEnabled = true;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

            OKButton.IsEnabled = false;
            base.OnClosing(e);
        }


        private Color m_color = new Color();
        private byte A_color = new byte();
        private byte R_color = new byte();
        private byte G_color = new byte();
        private byte B_color = new byte();
        private Color startingColor = new Color();

        public Color SelectedColor
        {
            get
            {
                return m_color;
            }

        }

        public byte SelectedColorA
        {
            get
            {
                return A_color;
            }

        }
        public byte SelectedColorR
        {
            get
            {
                return R_color;
            }

        }

        public byte SelectedColorG
        {
            get
            {
                return G_color;
            }

        }
        public byte SelectedColorB
        {
            get
            {
                return B_color;
            }

        }


        public Color StartingColor
        {
            get
            {
                return startingColor;
            }
            set
            {
                cPicker.SelectedColor = value;
                OKButton.IsEnabled = true;//false; Changed by CLK on 2016.11.14 to address [ID: 1601UD] "LED color cannot change from White to Yellow."
            }
        }

        private void gridTitle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragWindow(sender, e);
        }

        private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
