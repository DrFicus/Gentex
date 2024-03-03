namespace GeometryCalculator.Shapes
{
    public class EquilateralTriangle : Shape
    {
        public EquilateralTriangle(int id) : base(id) { }

        public override double CalculateArea()
        {
            double sideLength = Properties["Side Length"];
            return (Math.Sqrt(3) / 4) * sideLength * sideLength;
        }

        public override double CalculatePerimeter()
        {
            double sideLength = Properties["Side Length"];
            return 3 * sideLength;
        }

        public override (double X, double Y) CalculateCentroid()
        {
            return (Properties["CenterX"], Properties["CenterY"]);
        }

        public override void ValidateProperties(int lineNumber)
        {
            if (!Properties.ContainsKey("Side Length") || !Properties.ContainsKey("CenterX") || !Properties.ContainsKey("CenterY"))
            {
                throw new InvalidOperationException("EquilateralTriangle is missing required properties.");
            }
        }
    }
}
