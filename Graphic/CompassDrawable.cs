#nullable enable

using AlwaysInTarget.Helpers;
using AlwaysInTarget.ViewModels;

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
        private decimal WindStrength { get; set; }

        private string WindCorrectionAngel { get; set; } = string.Empty;
        private string BombSightDeflection { get; set; } = string.Empty;

        private int IndicatedAirspeed { get; set; }
        private int TrueAirSpeed { get; set; }
        private int Altitude { get; set; }

        private bool IsMapHeadingEnabled { get; set; }

        public void SetCurrentFlightParameters(NavigationOnlineModel navigationOnlineModel)
        {
            Heading = SafeConvertToFloat(navigationOnlineModel.Course);
            TrueCourse = SafeConvertToFloat(navigationOnlineModel.TrueCourse);

            if (float.TryParse(navigationOnlineModel.TrueHeading, out float trueHeading))
                TrueHeading = trueHeading;

            WindDirection = SafeConvertToFloat(navigationOnlineModel.WindDirection);
            WindStrength = Math.Round(navigationOnlineModel.WindStrenght, 1);

            WindCorrectionAngel = navigationOnlineModel.WindCorrectionAngel;
            BombSightDeflection = navigationOnlineModel.BombSightDeflection;

            IndicatedAirspeed = navigationOnlineModel.IAS;
            TrueAirSpeed = navigationOnlineModel.TAS;

            Altitude = navigationOnlineModel.Altitude;

            IsMapHeadingEnabled = navigationOnlineModel.IsMapHeadingEnabled;
        }

        private static float SafeConvertToFloat(object? value)
        {
            if (value == null) return 0f;
            try
            {
                return Convert.ToSingle(value);
            }
            catch
            {
                return 0f;
            }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            try
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

                if (IsMapHeadingEnabled)
                    DrawTrueCourseNeedle(canvas, centerX, centerY, radius);

                // litery N, E, S, W
                AddingCompassMarkings(canvas, centerX, centerY, radius);

                canvas.RestoreState();

                // --- 3) Wyświetlenie kursu w środku strzałki ---
                DisplayingTheAircraftHeading(canvas, centerX, centerY);

                BombsightDeflectionIndicator(canvas, centerX, centerY, radius);
                BombsightWindStrngthIndicator(canvas, centerX, centerY, radius);
                BombsightWindDirectionIndicator(canvas, centerX, centerY, radius);

                DisplayIndicatedAirspeed(canvas, centerX, centerY, radius);
                DisplayTrueAirSpeed(canvas, centerX, centerY, radius);

                DisplayHeading(canvas, centerX, centerY, radius);
                DisplayIndicatedAltitude(canvas, centerX, centerY, radius);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"[CompassDrawable] Draw error: {e}");
            }
        }

        private void DisplayIndicatedAirspeed(ICanvas canvas, float centerX, float centerY, float radius)
        {
            float boxWidth = 80f;
            float boxHeight = 20f;
            float offsetX = radius / 8f; // odległość od środka do prostokąta
            float offsetY = radius / 2f; // odległość od środka do prostokąta

            RectF iasRect = new RectF(centerX - offsetX - boxWidth, centerY + offsetY - boxHeight, boxWidth, boxHeight);

            canvas.FillColor = Colors.White;
            canvas.FillRectangle(iasRect);

            canvas.StrokeColor = Colors.WhiteSmoke;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(iasRect);

            canvas.FontColor = Colors.Black;
            canvas.FontSize = 15;
            canvas.DrawString(
                $"IAS: {IndicatedAirspeed}"
                , iasRect
                , HorizontalAlignment.Center
                , VerticalAlignment.Center
                );
        }

        public void DisplayTrueAirSpeed(ICanvas canvas, float centerX, float centerY, float radius)
        {
            float boxWidth = 80f;
            float boxHeight = 20f;
            float offsetX = radius / 8f; // odległość od środka do prostokąta
            float offsetY = radius / 2f; // odległość od środka do prostokąta

            RectF iasRect = new RectF(centerX + offsetX, centerY + offsetY - boxHeight, boxWidth, boxHeight);

            canvas.FillColor = Colors.White;
            canvas.FillRectangle(iasRect);

            canvas.StrokeColor = Colors.WhiteSmoke;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(iasRect);

            canvas.FontColor = Colors.Black;
            canvas.FontSize = 15;
            canvas.DrawString(
                $"Tas: {TrueAirSpeed}"
                , iasRect
                , HorizontalAlignment.Center
                , VerticalAlignment.Center
                );
        }

        private void DisplayHeading(ICanvas canvas, float centerX, float centerY, float radius)
        {
            float boxWidth = 80f;
            float boxHeight = 20f;
            float offsetX = radius / 8f; // odległość od środka do prostokąta
            float offsetY = radius / 2f; // odległość od środka do prostokąta

            RectF iasRect = new RectF(centerX - offsetX - boxWidth, centerY - offsetY, boxWidth, boxHeight);

            canvas.FillColor = Colors.White;
            canvas.FillRectangle(iasRect);

            canvas.StrokeColor = Colors.WhiteSmoke;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(iasRect);

            canvas.FontColor = Colors.Black;
            canvas.FontSize = 15;
            canvas.DrawString(
                $"HDG: {Heading}"
                , iasRect
                , HorizontalAlignment.Center
                , VerticalAlignment.Center
                );
        }

        private void DisplayIndicatedAltitude(ICanvas canvas, float centerX, float centerY, float radius)
        {
            float boxWidth = 80f;
            float boxHeight = 22f;
            float offsetX = radius / 8f; // odległość od środka do prostokąta
            float offsetY = radius / 2f; // odległość od środka do prostokąta

            RectF altRect = new RectF(centerX + offsetX, centerY - offsetY, boxWidth, boxHeight);

            canvas.FillColor = Colors.White;
            canvas.FillRectangle(altRect);

            canvas.StrokeColor = Colors.WhiteSmoke;
            canvas.StrokeSize = 2;
            canvas.DrawRectangle(altRect);

            canvas.FontColor = Colors.Black;
            canvas.FontSize = 15;
            canvas.DrawString(
                $"Alt: {NumberFormatter.FormatWithSpaces(Altitude)}"
                , altRect
                , HorizontalAlignment.Center
                ,VerticalAlignment.Center
                );
        }

        private void BombsightDeflectionIndicator(ICanvas canvas, float centerX, float centerY, float radius)
        {
            if (string.IsNullOrWhiteSpace(BombSightDeflection))
                return; // nic nie rysuj jeśli brak danych

            float boxWidth = 60f;
            float boxHeight = 30f;
            float offset = radius / 3f; // odległość od środka do prostokąta

            RectF portRect = new RectF(centerX - offset - boxWidth, centerY - boxHeight / 2f, boxWidth, boxHeight);
            RectF strbRect = new RectF(centerX + offset, centerY - boxHeight / 2f, boxWidth, boxHeight);

            if (BombSightDeflection.Contains("Strb") || BombSightDeflection.Length == 3)
            {
                canvas.FillColor = Colors.Red;
                canvas.FillRectangle(strbRect);

                canvas.StrokeColor = Colors.White;
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

                canvas.StrokeColor = Colors.White;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(portRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString(BombSightDeflection, portRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
        }

        public void BombsightWindDirectionIndicator(ICanvas canvas, float centerX, float centerY, float radius)
        {
            if (string.IsNullOrWhiteSpace(BombSightDeflection))
                return; // brak danych → nic nie rysujemy

            float boxWidth = 60f;
            float boxHeight = 30f;
            float offset = radius / 3f; // odległość od środka do prostokąta

            if (BombSightDeflection.Contains("Strb") || BombSightDeflection.Length == 3)
            {
                RectF portRect = new RectF(centerX - offset - boxWidth, centerY - (boxHeight + boxHeight / 6f), boxWidth, boxHeight);

                canvas.FillColor = Colors.Blue;
                canvas.FillRectangle(portRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(portRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString($"{WindDirection}°", portRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            else
            {
                RectF strbRect = new RectF(centerX + offset, centerY - (boxHeight + boxHeight / 6f), boxWidth, boxHeight);

                canvas.FillColor = Colors.Blue;
                canvas.FillRectangle(strbRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(strbRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString($"{WindDirection}°", strbRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
        }

        public void BombsightWindStrngthIndicator(ICanvas canvas, float centerX, float centerY, float radius)
        {
            if (string.IsNullOrWhiteSpace(BombSightDeflection))
                return; // brak danych → nic nie rysujemy

            float boxWidth = 60f;
            float boxHeight = 30f;
            float offset = radius / 3f; // odległość od środka do prostokąta
      
            if (BombSightDeflection.Contains("Strb") || BombSightDeflection.Length == 3)
            {
                RectF portRect = new RectF(centerX - offset - boxWidth, centerY + (boxHeight / 6f), boxWidth, boxHeight);

                canvas.FillColor = Colors.Blue;
                canvas.FillRectangle(portRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(portRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString($"{WindStrength} m/s", portRect, HorizontalAlignment.Center, VerticalAlignment.Center);
            }
            else
            {
                RectF strbRect = new RectF(centerX + offset, centerY + (boxHeight / 6f), boxWidth, boxHeight);

                canvas.FillColor = Colors.Blue;
                canvas.FillRectangle(strbRect);

                canvas.StrokeColor = Colors.WhiteSmoke;
                canvas.StrokeSize = 2;
                canvas.DrawRectangle(strbRect);

                canvas.FontColor = Colors.White;
                canvas.FontSize = 13;
                canvas.DrawString($"{WindStrength} m/s", strbRect, HorizontalAlignment.Center, VerticalAlignment.Center);
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

            canvas.FillColor = Colors.Blue; // np. niebieski dla wiatru
            canvas.FillPath(triangle);
            canvas.StrokeColor = Colors.White;
            canvas.StrokeSize = 1;
            canvas.DrawPath(triangle);
        }

        public void AddingCompassMarkings(ICanvas canvas, float centerX, float centerY, float radius)
        {
            canvas.FontColor = Colors.White;
            canvas.FontSize = 16;
            float labelRadius = radius + 18f;

            for (int deg = 0; deg < 360; deg += 30)
            {
                string text;
                switch (deg)
                {
                    case 0:
                        text = "N";
                        break;
                    case 90:
                        text = "E";
                        break;
                    case 180:
                        text = "S";
                        break;
                    case 270:
                        text = "W";
                        break;
                    default:
                        //text = deg.ToString();
                        text = "";
                        break;
                }

                double angle = (deg - 90) * Math.PI / 180.0;
                float lx = centerX + (float)Math.Cos(angle) * labelRadius;
                float ly = centerY + (float)Math.Sin(angle) * labelRadius;

                canvas.DrawString(
                    text,
                    lx - 20,
                    ly - 20,
                    40,
                    40,
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

        private void DrawTrueCourseNeedle(ICanvas canvas, float centerX, float centerY, float radius)
        {
            if (TrueCourse < 0 || TrueCourse > 360) return;

            canvas.SaveState();

            // ustaw środek jako punkt odniesienia
            canvas.Translate(centerX, centerY);

            // obróć znacznik o TrueCourse
            canvas.Rotate(TrueCourse);

            // parametry znaczników
            float markerLength = 12f;   // długość prostokąta w kierunku promienia
            float markerWidth = 5f;     // szerokość prostokąta wzdłuż obwodu
            float gap = 5f;             // przerwa między prostokątami

            // położenie prostokątów po wewnętrznej stronie obwodu
            float yStart = -radius + 5f + markerLength;

            // lewy prostokąt (po lewej stronie przerwy)
            RectF rectLeft = new RectF(-markerWidth - gap / 2f, yStart, markerWidth, markerLength);
            canvas.FillColor = Colors.LimeGreen;
            canvas.FillRectangle(rectLeft);

            // prawy prostokąt (po prawej stronie przerwy)
            RectF rectRight = new RectF(gap / 2f, yStart, markerWidth, markerLength);
            canvas.FillColor = Colors.LimeGreen;
            canvas.FillRectangle(rectRight);

            canvas.RestoreState();
        }
    }
}
