using GradientPicker.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GradientPicker.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            MenuPage page = new MenuPage();

            mainViewModel.CurrentPage = page;
            this.DataContext = mainViewModel;
        }

        
    }
}
