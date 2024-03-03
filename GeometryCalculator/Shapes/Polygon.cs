namespace GeometryCalculator.Shapes
{
    public class Polygon : Shape
    {
        private double? _cachedArea = null; // Nullable double to store the cached area

        public Polygon(int id) : base(id) { }

        public override double CalculateArea()
        {
            if (_cachedArea.HasValue)
            {
                return _cachedArea.Value;
            }

            // https://mathworld.wolfram.com/PolygonArea.html

            List<double> x = Properties.Where((kvp, index) => index % 2 == 0).Select(kvp => kvp.Value).ToList();
            List<double> y = Properties.Where((kvp, index) => index % 2 != 0).Select(kvp => kvp.Value).ToList();

            double area = 0;

            for (int i = 0; i < x.Count; i++)
            {
                int next = (i + 1) % x.Count; // The next vertex index, wrapping around to 0 at the end
                area += (x[i] * y[next] - x[next] * y[i]);
            }

            _cachedArea = Math.Abs(area / 2);

            return _cachedArea.Value;
        }

        public override double CalculatePerimeter()
        {
            List<double> x = Properties.Where((kvp, index) => index % 2 == 0).Select(kvp => kvp.Value).ToList();
            List<double> y = Properties.Where((kvp, index) => index % 2 != 0).Select(kvp => kvp.Value).ToList();

            double perimeter = 0;
            for (int i = 0, j = x.Count - 1; i < x.Count; j = i++)
            {
                double dx = x[j] - x[i];
                double dy = y[j] - y[i];
                perimeter += Math.Sqrt(dx * dx + dy * dy); // Pythagorean theorem
            }

            return perimeter;
        }

        public override (double X, double Y) CalculateCentroid()
        {

            // https://mathworld.wolfram.com/PolygonCentroid.html
            
            double area = _cachedArea ?? CalculateArea();
            double factor = 1 / (6 * area);
            double Cx = 0, Cy = 0;

            List<double> x = Properties.Where((kvp, index) => index % 2 == 0).Select(kvp => kvp.Value).ToList();
            List<double> y = Properties.Where((kvp, index) => index % 2 != 0).Select(kvp => kvp.Value).ToList();

            for (int i = 0, j = x.Count - 1; i < x.Count; j = i++)
            {
                double term = (x[j] * y[i] - x[i] * y[j]);
                Cx += (x[j] + x[i]) * term;
                Cy += (y[j] + y[i]) * term;
            }

            Cx *= factor;
            Cy *= factor;

            return (Cx, Cy);
        }

        public override void ValidateProperties(int lineNumber)
        {
            // Check if there's an even number of properties and at least 6 properties (to form a minimum of a triangle).
            if (Properties.Count % 2 != 0 || Properties.Count < 6)
            {
                throw new InvalidOperationException($"Line {lineNumber}: Polygon has an incorrect number of coordinates.");
            }

            // Verify that each expected X and Y coordinate is present
            for (int i = 0; i < Properties.Count / 2; i++)
            {
                string xKey = $"X{i}";
                string yKey = $"Y{i}";
                
                if (!Properties.ContainsKey(xKey))
                {
                    throw new InvalidOperationException($"Line {lineNumber}: Polygon is missing the {xKey} coordinate.");
                }
                if (!Properties.ContainsKey(yKey))
                {
                    throw new InvalidOperationException($"Line {lineNumber}: Polygon is missing the {yKey} coordinate.");
                }
            }

            // Check if the first and last coordinates are the same for closed polygons
            string firstXKey = "X0";
            string lastXKey = $"X{Properties.Count / 2 - 1}";
            string firstYKey = "Y0";
            string lastYKey = $"Y{Properties.Count / 2 - 1}";

            if (Properties[firstXKey] != Properties[lastXKey] || Properties[firstYKey] != Properties[lastYKey])
            {
                throw new InvalidOperationException($"Line {lineNumber}: Polygon's first and last coordinates should match to form a closed shape.");
            }
        }

    }
}
