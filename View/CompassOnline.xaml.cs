using AlwaysInTarget.Graphic;

namespace AlwaysInTarget.View;

public partial class CompassOnline : ContentPage
{
    private CompassDrawable _compassDrawable;
    public CompassOnline()
	{
        InitializeComponent();

        _compassDrawable = new CompassDrawable();
        CompassView.Drawable = _compassDrawable;
    }

    private void HeadingSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        _compassDrawable.Heading = (float)e.NewValue;
        CompassView.Invalidate(); // odœwie¿ rysowanie
    }
}