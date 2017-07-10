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
    public class GradientPickerControl : Control
    {

        internal bool _BrushSetInternally = false;
        internal bool _UpdateBrush = true;

        static GradientPickerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GradientPickerControl), new FrameworkPropertyMetadata(typeof(GradientPickerControl)));
            
        }

        public static RoutedCommand RemoveGradientStop = new RoutedCommand();
        public static RoutedCommand RearrangeGradientStop = new RoutedCommand();
        public static RoutedCommand AddGradientStop = new RoutedCommand();

        public void InitialBrush()
        {
            if (Gradients == null)
            {
                Gradients = new ObservableCollection<GradientStop>();
                Gradients.Add(new GradientStop(Colors.Black, 0));
                Gradients.Add(new GradientStop(Colors.White, 1));
            }

            SetBrush();
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            InitialBrush();
            this.CommandBindings.Add(new CommandBinding(GradientPickerControl.RemoveGradientStop, RemoveGradientStop_Executed));
            this.CommandBindings.Add(new CommandBinding(GradientPickerControl.RearrangeGradientStop, RearrangeGradientStop_Executed));
            this.CommandBindings.Add(new CommandBinding(GradientPickerControl.AddGradientStop, AddGradientStop_Executed));
        }

        private void RemoveGradientStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Gradients != null && this.Gradients.Count > 2)
            {
                this.Gradients.Remove(this.SelectedGradient);
                this.SetBrush();
            }
        }

        private void AddGradientStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            GradientPickerControl cl = sender as GradientPickerControl;

            GradientStop _gs = new GradientStop();
            _gs.Offset = Mouse.GetPosition(cl).X/cl.ActualWidth;
            _gs.Color = this.Color;
            this.Gradients.Add(_gs);
            this.SelectedGradient = _gs;
            this.Color = _gs.Color;
            this.SetBrush();
        }

        private void RearrangeGradientStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this._UpdateBrush = false;
            this._BrushSetInternally = true;
            int nGradients = Gradients.Count;
            Gradients[0].Offset = 0;
            for (int i = 1; i < nGradients - 1; i++)
            {
                Gradients[i].Offset = (double) i / (nGradients - 1);
            }
            Gradients[nGradients - 1].Offset = 1;

            //foreach (GradientStop gs in Gradients)
            //{
            //    gs.Offset = 1.0 - gs.Offset;
            //}
            this._UpdateBrush = true;
            this._BrushSetInternally = false;
            this.SetBrush();
        }

        public IEnumerable<Enum> SpreadMethodTypes
        {
            get
            {
                GradientSpreadMethod temp = GradientSpreadMethod.Pad | GradientSpreadMethod.Reflect | GradientSpreadMethod.Repeat;
                foreach (Enum value in Enum.GetValues(temp.GetType()))
                    if (temp.HasFlag(value))
                        yield return value;
            }
        }

        public IEnumerable<Enum> MappingModeTypes
        {
            get
            {
                BrushMappingMode temp = BrushMappingMode.Absolute | BrushMappingMode.RelativeToBoundingBox;
                foreach (Enum value in Enum.GetValues(temp.GetType()))
                    if (temp.HasFlag(value))
                        yield return value;
            }
        }

        #region Private Properties

        double StartX
        {
            get { return (double)GetValue(StartXProperty); }
            set { SetValue(StartXProperty, value); }
        }
        static readonly DependencyProperty StartXProperty =
            DependencyProperty.Register("StartX", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(StartXChanged)));
        static void StartXChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is LinearGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as LinearGradientBrush).StartPoint = new Point((double)args.NewValue, (cp.Brush as LinearGradientBrush).StartPoint.Y);
                cp._BrushSetInternally = false;
            }
        }

        double StartY
        {
            get { return (double)GetValue(StartYProperty); }
            set { SetValue(StartYProperty, value); }
        }
        static readonly DependencyProperty StartYProperty =
            DependencyProperty.Register("StartY", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.0, new PropertyChangedCallback(StartYChanged)));
        static void StartYChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is LinearGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as LinearGradientBrush).StartPoint = new Point((cp.Brush as LinearGradientBrush).StartPoint.X, (double)args.NewValue);
                cp._BrushSetInternally = false;
            }
        }

        double EndX
        {
            get { return (double)GetValue(EndXProperty); }
            set { SetValue(EndXProperty, value); }
        }
        static readonly DependencyProperty EndXProperty =
            DependencyProperty.Register("EndX", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(EndXChanged)));
        static void EndXChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is LinearGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as LinearGradientBrush).EndPoint = new Point((double)args.NewValue, (cp.Brush as LinearGradientBrush).EndPoint.Y);
                cp._BrushSetInternally = false;
            }
        }

        double EndY
        {
            get { return (double)GetValue(EndYProperty); }
            set { SetValue(EndYProperty, value); }
        }
        static readonly DependencyProperty EndYProperty =
            DependencyProperty.Register("EndY", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(1.0, new PropertyChangedCallback(EndYChanged)));
        static void EndYChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is LinearGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as LinearGradientBrush).EndPoint = new Point((cp.Brush as LinearGradientBrush).EndPoint.X, (double)args.NewValue);
                cp._BrushSetInternally = false;
            }
        }



        double GradientOriginX
        {
            get { return (double)GetValue(GradientOriginXProperty); }
            set { SetValue(GradientOriginXProperty, value); }
        }
        static readonly DependencyProperty GradientOriginXProperty =
            DependencyProperty.Register("GradientOriginX", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(GradientOriginXChanged)));
        static void GradientOriginXChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is RadialGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as RadialGradientBrush).GradientOrigin = new Point((double)args.NewValue, (cp.Brush as RadialGradientBrush).GradientOrigin.Y);
                cp._BrushSetInternally = false;
            }
        }

        double GradientOriginY
        {
            get { return (double)GetValue(GradientOriginYProperty); }
            set { SetValue(GradientOriginYProperty, value); }
        }
        static readonly DependencyProperty GradientOriginYProperty =
            DependencyProperty.Register("GradientOriginY", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(GradientOriginYChanged)));
        static void GradientOriginYChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is RadialGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as RadialGradientBrush).GradientOrigin = new Point((cp.Brush as RadialGradientBrush).GradientOrigin.X, (double)args.NewValue);
                cp._BrushSetInternally = false;
            }
        }

        double CenterX
        {
            get { return (double)GetValue(CenterXProperty); }
            set { SetValue(CenterXProperty, value); }
        }
        static readonly DependencyProperty CenterXProperty =
            DependencyProperty.Register("CenterX", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(CenterXChanged)));
        static void CenterXChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is RadialGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as RadialGradientBrush).Center = new Point((double)args.NewValue, (cp.Brush as RadialGradientBrush).Center.Y);
                cp._BrushSetInternally = false;
            }
        }

        double CenterY
        {
            get { return (double)GetValue(CenterYProperty); }
            set { SetValue(CenterYProperty, value); }
        }
        static readonly DependencyProperty CenterYProperty =
            DependencyProperty.Register("CenterY", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(CenterYChanged)));
        static void CenterYChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is RadialGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as RadialGradientBrush).Center = new Point((cp.Brush as RadialGradientBrush).Center.X, (double)args.NewValue);
                cp._BrushSetInternally = false;
            }
        }

        double RadiusX
        {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }
        static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.Register("RadiusX", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(RadiusXChanged)));
        static void RadiusXChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is RadialGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as RadialGradientBrush).RadiusX = (double)args.NewValue;
                cp._BrushSetInternally = false;
            }
        }

        double RadiusY
        {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }
        static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.Register("RadiusY", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(0.5, new PropertyChangedCallback(RadiusYChanged)));
        static void RadiusYChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is RadialGradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as RadialGradientBrush).RadiusY = (double)args.NewValue;
                cp._BrushSetInternally = false;
            }
        }

        double BrushOpacity
        {
            get { return (double)GetValue(BrushOpacityProperty); }
            set { SetValue(BrushOpacityProperty, value); }
        }
        static readonly DependencyProperty BrushOpacityProperty =
            DependencyProperty.Register("BrushOpacity", typeof(double), typeof(GradientPickerControl), new PropertyMetadata(1.0));
        //static void BrushOpacityChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        //{
        //    ColorBox cp = property as ColorBox;
        //    cp._BrushSetInternally = true;
        //    cp.Brush.Opacity = (double)args.NewValue;
        //    cp._BrushSetInternally = false;            
        //}

        GradientSpreadMethod SpreadMethod
        {
            get { return (GradientSpreadMethod)GetValue(SpreadMethodProperty); }
            set { SetValue(SpreadMethodProperty, value); }
        }
        static readonly DependencyProperty SpreadMethodProperty =
            DependencyProperty.Register("SpreadMethod", typeof(GradientSpreadMethod), typeof(GradientPickerControl), new PropertyMetadata(GradientSpreadMethod.Pad, new PropertyChangedCallback(SpreadMethodChanged)));
        static void SpreadMethodChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is GradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as GradientBrush).SpreadMethod = (GradientSpreadMethod)args.NewValue;
                cp._BrushSetInternally = false;
            }
        }

        BrushMappingMode MappingMode
        {
            get { return (BrushMappingMode)GetValue(MappingModeProperty); }
            set { SetValue(MappingModeProperty, value); }
        }
        static readonly DependencyProperty MappingModeProperty =
            DependencyProperty.Register("MappingMode", typeof(BrushMappingMode), typeof(GradientPickerControl), new PropertyMetadata(BrushMappingMode.RelativeToBoundingBox, new PropertyChangedCallback(MappingModeChanged)));
        static void MappingModeChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            GradientPickerControl cp = property as GradientPickerControl;
            if (cp.Brush is GradientBrush)
            {
                cp._BrushSetInternally = true;
                (cp.Brush as GradientBrush).MappingMode = (BrushMappingMode)args.NewValue;
                cp._BrushSetInternally = false;
            }
        }

        #endregion


        internal ObservableCollection<GradientStop> Gradients
        {
            get { return (ObservableCollection<GradientStop>)GetValue(GradientsProperty); }
            set { SetValue(GradientsProperty, value); }
        }
        internal static readonly DependencyProperty GradientsProperty =
            DependencyProperty.Register("Gradients", typeof(ObservableCollection<GradientStop>), typeof(GradientPickerControl));

        internal GradientStop SelectedGradient
        {
            get { return (GradientStop)GetValue(SelectedGradientProperty); }
            set { SetValue(SelectedGradientProperty, value); }
        }
        internal static readonly DependencyProperty SelectedGradientProperty =
            DependencyProperty.Register("SelectedGradient", typeof(GradientStop), typeof(GradientPickerControl));


        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set { SetValue(BrushProperty, value); }
        }
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(GradientPickerControl));

        //static void BrushChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        //{
        //    GradientPickerControl c = property as GradientPickerControl;
        //    Brush brush = args.NewValue as Brush;
        //    if (!c._BrushSetInternally)
        //    {
        //        LinearGradientBrush lgb = brush as LinearGradientBrush;
        //        c.StartX = lgb.StartPoint.X;
        //        c.StartY = lgb.StartPoint.Y;
        //        c.EndX = lgb.EndPoint.X;
        //        c.EndY = lgb.EndPoint.Y;
        //        c.MappingMode = lgb.MappingMode;
        //        c.SpreadMethod = lgb.SpreadMethod;
        //        c.Gradients = new ObservableCollection<GradientStop>(lgb.GradientStops);
        //    }
        //}

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(GradientPickerControl));

        internal void SetBrush()
        {
            if (!_UpdateBrush)
                return;

            this._BrushSetInternally = true;

            // retain old opacity
            double opacity = 1;
            TransformGroup tempTG = null;
            if (this.Brush != null)
            {
                opacity = this.Brush.Opacity;
                tempTG = Brush.Transform as TransformGroup;
            }

            var brush = new LinearGradientBrush();
            foreach (GradientStop g in Gradients)
            {
                brush.GradientStops.Add(new GradientStop(g.Color, g.Offset));
            }
            brush.StartPoint = new Point(this.StartX, this.StartY);
            brush.EndPoint = new Point(this.EndX, this.EndY);
            brush.MappingMode = this.MappingMode;
            brush.SpreadMethod = this.SpreadMethod;
            Brush = brush;
            this.Brush.Opacity = opacity;
            this._BrushSetInternally = false;
        }
    }
}
