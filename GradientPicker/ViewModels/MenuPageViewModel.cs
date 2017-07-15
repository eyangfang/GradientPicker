using System.ComponentModel;
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

        private Color _CurrentColor_Stops;
        public Color CurrentColor_Stops
        {
            get { return this._CurrentColor_Stops; }
            set
            {
                this._CurrentColor_Stops = value;
                this.OnPropertyChanged("CurrentColor_Stops");
                this._CurrentColor_Swatches = value;
                this.OnPropertyChanged("CurrentColor_Swatches");
            }
        }

        private Color _CurrentColor_Swatches;
        public Color CurrentColor_Swatches
        {
            get { return this._CurrentColor_Swatches; }
            set
            {
                this._CurrentColor_Swatches = value;
                this.OnPropertyChanged("CurrentColor_Swatches");
                this._CurrentColor_Stops = value;
                this.OnPropertyChanged("CurrentColor_Stops");
            }
        }

    }
}
