using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;


namespace GradientPicker.ViewModels
{
    public class MenuPageViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Color _CurrentColor;
        public Color CurrentColor
        {
            get { return this._CurrentColor; }
            set
            {
                this._CurrentColor = value;
                this.OnPropertyChanged("CurrentColor");
            }
        }
        
    }
}
