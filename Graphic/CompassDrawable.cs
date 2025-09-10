#nullable disable

using AlwaysInTarget.ViewModels;
//using Java.Security;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Graphics;

namespace AlwaysInTarget.Graphic
{
    public class CompassDrawable : IDrawable
    {
        private const float Margin = 30f;
        private const float MarginFlightDirectionArrow = 12f;

        public float TrueHeading { get; set; } //Kurs po uwzględnieniu wiatru
        private float TrueCourse { get; set; } //kurs z mapy
        private float Heading { get; set; } //kurs w stopniach
        private float WindDirection { get; set; }
        private float WindStrength { get; set; }

        private string WindCorrectionAngel { get; set; }
        private string BombSightDeflection { get; set; }

        public void SetCurrentFlightParameters(NavigationOnlineModel navigationOnlineModel)
        {
            Heading = Convert.ToSingle(navigationOnlineModel.Course);
            TrueCourse = Convert.ToSingle(navigationOnlineModel.TrueCourse);

            if (Single.TryParse(navigationOnlineModel.TrueHeading, out float trueHeading))
            {
                TrueHeading = trueHeading;
            }

            WindDirection = Convert.ToSingle(navigationOnlineModel.WindDirection);
            WindStrength = Convert.ToSingle(navigationOnlineModel.WindStrenght);

            WindCorrectionAngel = navigationOnlineModel.WindCorrectionAngel;
            BombSightDeflection = navigationOnlineModel.BombSightDeflection;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float w = dirtyRect.Width;
            float h = dirtyRect.Height;
            float centerX = dirtyRect.Center.X;
            float centerY = dirtyRect.Center.Y;

            float radius = Math.Min(w, h) / 2f - Margin;
            if (radius <= 0) return;

            // tło
            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(dirtyRect);

            // --- 1) Strzałka kierunku lotu (czerwona) ---
            FlightDirectionArrow(canvas, centerX, centerY, radius - 10, MarginFlightDirectionArrow, Convert.ToUInt32(TrueHeading).ToString());

            // --- 2) Obrót całego koła o -Heading ---
            RotationOfTheCompassWheel(canvas, centerX, centerY);

            // obwód kompasu
            CompassCircumference(canvas, centerX, centerY, radius);

            // kreski co 1°, dłuższe co 5°
            ScaleOnTheCompassWheel(canvas, centerX, centerY, radius);

            //Kierunek wiatru
            DrawWindDirectionMarker(canvas, centerX, centerY, radius);

            // litery N, E, S, W
            AddingCompassMarkings(canvas, centerX, centerY, radius);

            canvas.RestoreState();

            // --- 3) Wyświetlenie kursu w środku strzałki ---
            DisplayingTheAircraftHeading(canvas, centerX, centerY);
            BombsightDeflectionIndicator(canvas, centerX, centerY, radius);
        }

        private void BombsightDeflectionIndicator(ICanvas canvas, float centerX, float centerY, float radius)
        {
            float boxWidth = 60f;
            float boxHeight = 30f;
            float offset = radius / 3f; // odległość od środka do prostokąta

            RectF portRect = new RectF(centerX - offset - boxWidth, centerY - boxHeight / 2f, boxWidth, boxHeight);
            RectF strbRect = new RectF(centerX + offset, centerY - boxHeight / 2f, boxWidth, boxHeight);

            if(BombSightDeflection.Length == 1 || BombSightDeflection.Length == 3)
            {
                canvas.FillColor = Colors.Red;
                canvas.FillRectangle(strbRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(strbRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString(BombSightDeflection, strbRect, HorizontalAlignment.Center, VerticalAlignment.Center);

                canvas.FillColor = Colors.Red;
                canvas.FillRectangle(portRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(portRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString(BombSightDeflection, portRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            else if (BombSightDeflection.Contains("Strb"))
            {
                canvas.FillColor = Colors.Red;
                canvas.FillRectangle(strbRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(strbRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString(BombSightDeflection, strbRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            else
            {
                canvas.FillColor = Colors.Red;
                canvas.FillRectangle(portRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(portRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString(BombSightDeflection, portRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
        }

        public void DisplayingTheAircraftHeading(ICanvas canvas, float centerX, float centerY)
        {
            canvas.FontColor = Colors.White;
            canvas.FontSize = 16;
            canvas.DrawString(
                "TRK:" + TrueHeading.ToString("0") + "°",
                centerX - 30,
                centerY - 10,
                80,
                20,
                HorizontalAlignment.Justified,
                VerticalAlignment.Center,
                TextFlow.ClipBounds,
                0);
        }

        public void RotationOfTheCompassWheel(ICanvas canvas, float centerX, float centerY)
        {
            canvas.SaveState();
            canvas.Translate(centerX, centerY);
            canvas.Rotate(-TrueHeading); // minus: koło obraca się w przeciwną stronę niż kurs
            canvas.Translate(-centerX, -centerY);
        }

        private void DrawWindDirectionMarker(ICanvas canvas, float centerX, float centerY, float radius)
        {
            if (WindDirection < 0 || WindDirection > 360) return;

            // kąt w radianach (0° = północ, zgodnie z układem jak w kompasie)
            double rad = (WindDirection - 90) * Math.PI / 180.0;
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);

            // pozycja czubka trójkąta (bliżej środka)
            float tipX = centerX + cos * (radius - 20);
            float tipY = centerY + sin * (radius - 20);

            // baza trójkąta leży bliżej obwodu
            float baseX = centerX + cos * (radius - 10);
            float baseY = centerY + sin * (radius - 10);

            // szerokość markera
            float arrowWidth = 12f;

            // wektor prostopadły do kierunku
            float px = -sin;
            float py = cos;

            var triangle = new PathF();
            triangle.MoveTo(tipX, tipY); // czubek w stronę środka
            triangle.LineTo(baseX + px * (arrowWidth / 2f), baseY + py * (arrowWidth / 2f));
            triangle.LineTo(baseX - px * (arrowWidth / 2f), baseY - py * (arrowWidth / 2f));
            triangle.Close();

            canvas.FillColor = Colors.DeepSkyBlue; // np. niebieski dla wiatru
            canvas.FillPath(triangle);
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1;
            canvas.DrawPath(triangle);
        }

        public void AddingCompassMarkings(ICanvas canvas, float centerX, float centerY, float radius)
        {
            //string[] dirs = { "N", "E", "S", "W" };
            //canvas.FontColor = Colors.White;
            //canvas.FontSize = 18;
            //float labelRadius = radius + 18f;

            //for (int i = 0; i < 4; i++)
            //{
            //    double angle = (i * 90 - 90) * Math.PI / 180.0;
            //    float lx = centerX + (float)Math.Cos(angle) * labelRadius;
            //    float ly = centerY + (float)Math.Sin(angle) * labelRadius;
            //    canvas.DrawString(
            //        dirs[i],
            //        lx - 20,
            //        ly - 20,
            //        40,
            //        40,
            //        HorizontalAlignment.Center,
            //        VerticalAlignment.Center,
            //        TextFlow.ClipBounds,
            //        0);
            //}

            float mainLabelRadius = radius + 18f;
            float extraLabelRadius = radius + 12f; // mniejsze liczby dla pośrednich kierunków

            // główne kierunki
            var mainDirs = new Dictionary<int, string>
    {
        { 0, "N" },
        { 90, "E" },
        { 180, "S" },
        { 270, "W" }
    };

            // pośrednie kąty
            var extraDirs = new Dictionary<int, string>
    {
        { 30, "30" },
        { 60, "60" },
        { 120, "120" },
        { 150, "150" },
        { 210, "210" },
        { 240, "240" },
        { 300, "300" },
        { 330, "330" }
    };

            // rysujemy główne kierunki
            canvas.FontColor = Colors.White;
            canvas.FontSize = 18;
            foreach (var kvp in mainDirs)
            {
                double angle = (kvp.Key - 90) * Math.PI / 180.0;
                float lx = centerX + (float)Math.Cos(angle) * mainLabelRadius;
                float ly = centerY + (float)Math.Sin(angle) * mainLabelRadius;

                canvas.DrawString(
                    kvp.Value,
                    lx - 12,
                    ly - 12,
                    24,
                    24,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Center,
                    TextFlow.ClipBounds,
                    0);
            }

            // rysujemy pośrednie kąty
            canvas.FontSize = 12; // mniejsze liczby
            foreach (var kvp in extraDirs)
            {
                double angle = (kvp.Key - 90) * Math.PI / 180.0;
                float lx = centerX + (float)Math.Cos(angle) * extraLabelRadius;
                float ly = centerY + (float)Math.Sin(angle) * extraLabelRadius;

                canvas.DrawString(
                    kvp.Value,
                    lx - 10,
                    ly - 10,
                    20,
                    20,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Center,
                    TextFlow.ClipBounds,
                    0);
            }
        }

        private void FlightDirectionArrow(ICanvas canvas,
            float centerX,
            float centerY,
            float radius,
            float Margin,
            string headingText)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4;

            // --- 1) Strzałka pionowa (N-S) z przerwą na kurs ---
            float gapHeight = 30f; // wysokość przerwy na kurs

            float topY = centerY - (radius - Margin);
            float bottomY = centerY + (radius - Margin);

            // linia od góry do przerwy
            canvas.DrawLine(centerX, topY, centerX, centerY - gapHeight / 2f);

            // linia od dołu do przerwy
            canvas.DrawLine(centerX, bottomY, centerX, centerY + gapHeight / 2f);

            // --- 2) Grot trójkątny na górze strzałki ---
            float arrowHeight = 32f;
            float arrowWidth = 14f;

            float tipX = centerX;
            float tipY = topY; // grot na samej górze

            float baseX = tipX;
            float baseY = tipY + arrowHeight;

            var triangle = new PathF();
            triangle.MoveTo(tipX, tipY);
            triangle.LineTo(baseX - arrowWidth / 2f, baseY);
            triangle.LineTo(baseX + arrowWidth / 2f, baseY);
            triangle.Close();

            canvas.FillColor = Colors.Red;
            canvas.FillPath(triangle);
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1;
            canvas.DrawPath(triangle);
        }

        private void CompassCircumference(ICanvas canvas, float centerX, float centerY, float radius)
        {
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 2;
            canvas.DrawCircle(centerX, centerY, radius);
        }

        private void ScaleOnTheCompassWheel(ICanvas canvas, float centerX, float centerY, float radius)
        {
            float shortTick = 8f;
            float longTick = 15f;
            for (int deg = 0; deg < 360; deg++)
            {
                float tickLen = (deg % 5 == 0) ? longTick : shortTick;
                double rad = (deg - 90) * Math.PI / 180.0;
                float cos = (float)Math.Cos(rad);
                float sin = (float)Math.Sin(rad);

                float xOuter = centerX + cos * (radius + (tickLen / 2));
                float yOuter = centerY + sin * (radius + (tickLen / 2));
                float xInner = centerX + cos * (radius - (tickLen / 2));
                float yInner = centerY + sin * (radius - (tickLen / 2));

                canvas.StrokeSize = (deg % 5 == 0) ? 2 : 1;
                canvas.DrawLine(xInner, yInner, xOuter, yOuter);
            }
        }
    }
}
