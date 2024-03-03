namespace GeometryCalculator.Shapes
{
    public class Square : Shape
    {
        public Square(int id) : base(id) { }

        public override double CalculateArea()
        {
            double sideLength = Properties["Side Length"];
            return sideLength * sideLength;
        }

        public override double CalculatePerimeter()
        {
            double sideLength = Properties["Side Length"];
            return 4 * sideLength;
        }

        public override (double X, double Y) CalculateCentroid()
        {
            // The centroid calculation doesn't change with orientation for a square.
            return (Properties["CenterX"], Properties["CenterY"]);
        }

        public override void ValidateProperties(int lineNumber)
        {
            if (!Properties.ContainsKey("Side Length") || !Properties.ContainsKey("CenterX") || !Properties.ContainsKey("CenterY"))
            {
                throw new InvalidOperationException("Square is missing required properties.");
            }
        }
    }
}
