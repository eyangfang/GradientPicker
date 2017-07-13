using GradientPicker.ViewModels;

namespace GradientPicker
{
    /// <summary>
    /// Interaction logic for Page.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPageViewModel pageViewMode;
        public MenuPage()
        {
            InitializeComponent();
            pageViewMode = new MenuPageViewModel();
            this.DataContext = pageViewMode;
        }

    }
}
