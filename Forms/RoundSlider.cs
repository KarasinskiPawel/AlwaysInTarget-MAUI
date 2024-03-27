#nullable disable

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlwaysInTarget.Forms
{
    internal class RoundSlider : ContentView
    {
        private const double TwoPi = 2 * Math.PI;
        private const double StartAngle = -Math.PI / 2; // Start at 12 o'clock position
        private const double EndAngle = StartAngle + TwoPi;

        private double _angle;
        private BoxView _handle;

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(RoundSlider), 0.0,
                propertyChanged: OnValueChanged);

        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var slider = (RoundSlider)bindable;
            slider.UpdateHandlePosition((double)newValue);
            slider.ValueChanged?.Invoke(slider, new ValueChangedEventArgs((double)oldValue, (double)newValue));
        }

        public RoundSlider()
        {
            var circle = new BoxView
            {
                Color = Colors.LightGray,
                CornerRadius = 100,
                HeightRequest = 200,
                WidthRequest = 200,
            };

            _handle = new BoxView
            {
                Color = Colors.Blue,
                CornerRadius = 10,
                HeightRequest = 20,
                WidthRequest = 20,
            };

            var gestureRecognizer = new PanGestureRecognizer();
            gestureRecognizer.PanUpdated += OnPanUpdated;
            _handle.GestureRecognizers.Add(gestureRecognizer);

            Content = new AbsoluteLayout
            {
                Children = { circle, _handle }
            };

            UpdateHandlePosition(Value);
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    var x = e.TotalX;
                    var y = e.TotalY;
                    var angle = Math.Atan2(y, x);

                    // Convert angle to range [0, 2*PI]
                    if (angle < 0)
                    {
                        angle += TwoPi;
                    }

                    Value = (angle / TwoPi) * 100; // Assuming the value range is from 0 to 100
                    break;
            }
        }

        private void UpdateHandlePosition(double value)
        {
            _angle = StartAngle + (value / 100) * TwoPi;
            var radius = 100; // Radius of the circle
            int x = Convert.ToInt32(radius * Math.Cos(_angle) + radius);
            int y = Convert.ToInt32(radius * Math.Sin(_angle) + radius);

            AbsoluteLayout.SetLayoutBounds(_handle, new Rect(x, y, Convert.ToInt32(_handle.Width), Convert.ToInt32(_handle.Height)));

            //AbsoluteLayout.SetLayoutBounds(_handle, new Rectangle(x, y, Convert.ToInt32(_handle.Width), Convert.ToInt32(_handle.Height)));
        }
    }
}
