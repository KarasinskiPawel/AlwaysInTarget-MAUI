using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Graphics;

namespace AlwaysInTarget.Graphic
{
    public class CompassDrawable : IDrawable
    {
        public float Heading { get; set; } // kurs w stopniach

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float w = dirtyRect.Width;
            float h = dirtyRect.Height;
            float centerX = dirtyRect.Center.X;
            float centerY = dirtyRect.Center.Y;

            float margin = 30f; // margines od krawędzi
            float radius = Math.Min(w, h) / 2f - margin;
            if (radius <= 0) return;

            // tło
            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(dirtyRect);

            // --- 1) Strzałka N-S (fixed) ---
            //DrawArrow(canvas, centerX, centerY, radius - 10, Colors.Red, Colors.White);
            DrawLineWithArrow(canvas, centerX, centerY, radius - 10, margin, Convert.ToUInt32(Heading).ToString());

            // --- 2) Obrót całego koła o -Heading ---
            canvas.SaveState();
            canvas.Translate(centerX, centerY);
            canvas.Rotate(-Heading); // minus: koło obraca się w przeciwną stronę niż kurs
            canvas.Translate(-centerX, -centerY);

            // obwód kompasu
            CompassCircumference(canvas, centerX, centerY, radius);

            // kreski co 1°, dłuższe co 5°
            ScaleOnTheCompassWheel(canvas, centerX, centerY, radius);

            // litery N, E, S, W
            string[] dirs = { "N", "E", "S", "W" };
            canvas.FontColor = Colors.White;
            canvas.FontSize = 18;
            float labelRadius = radius + 18f;
            for (int i = 0; i < 4; i++)
            {
                double angle = (i * 90 - 90) * Math.PI / 180.0;
                float lx = centerX + (float)Math.Cos(angle) * labelRadius;
                float ly = centerY + (float)Math.Sin(angle) * labelRadius;
                canvas.DrawString(
                    dirs[i],
                    lx - 20,
                    ly - 20,
                    40,
                    40,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Center,
                    TextFlow.ClipBounds,
                    0);
            }

            canvas.RestoreState();

            // --- 3) Wyświetlenie kursu w środku strzałki ---
            canvas.FontColor = Colors.White;
            canvas.FontSize = 16;
            canvas.DrawString(
                Heading.ToString("0") + "°",
                centerX - 15,
                centerY - 10,
                30,
                20,
                HorizontalAlignment.Center,
                VerticalAlignment.Center,
                TextFlow.ClipBounds,
                0);
        }

        // Pomocnicza metoda do rysowania strzałki N-S
        private void DrawLineWithArrow(ICanvas canvas,
            float centerX,
            float centerY,
            float radius,
            float margin,
            string headingText)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4;

            // --- 1) Strzałka pionowa (N-S) z przerwą na kurs ---
            float gapHeight = 30f; // wysokość przerwy na kurs

            float topY = centerY - (radius - margin);
            float bottomY = centerY + (radius - margin);

            // linia od góry do przerwy
            canvas.DrawLine(centerX, topY, centerX, centerY - gapHeight / 2f);

            // linia od dołu do przerwy
            canvas.DrawLine(centerX, bottomY, centerX, centerY + gapHeight / 2f);

            // --- 2) Grot trójkątny na górze strzałki ---
            float arrowHeight = 12f;
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
                float xOuter = centerX + cos * radius;
                float yOuter = centerY + sin * radius;
                float xInner = centerX + cos * (radius - tickLen);
                float yInner = centerY + sin * (radius - tickLen);
                canvas.StrokeSize = (deg % 5 == 0) ? 2 : 1;
                canvas.DrawLine(xInner, yInner, xOuter, yOuter);
            }
        }
    }
}
