namespace GeometryCalculator.Shapes
{
    public class Ellipse : Shape
    {
        public Ellipse(int id) : base(id) { }

        public override double CalculateArea()
        {
            double semiMajorAxis = Properties["R1"];
            double semiMinorAxis = Properties["R2"];
            return Math.PI * semiMajorAxis * semiMinorAxis;
        }

        public override double CalculatePerimeter()
        {
            // Approximation using Ramanujan's formula
            double semiMajorAxis = Properties["R1"];
            double semiMinorAxis = Properties["R2"];
            return Math.PI * (3 * (semiMajorAxis + semiMinorAxis) - Math.Sqrt((3 * semiMajorAxis + semiMinorAxis) * (semiMajorAxis + 3 * semiMinorAxis)));
        }

        public override (double X, double Y) CalculateCentroid()
        {
            return (Properties["CenterX"], Properties["CenterY"]);
        }

        public override void ValidateProperties(int lineNumber)
        {
            if (!Properties.ContainsKey("R1") || !Properties.ContainsKey("R2") || !Properties.ContainsKey("CenterX") || !Properties.ContainsKey("CenterY"))
            {
                throw new InvalidOperationException("Ellipse is missing required properties.");
            }
        }
    }
}
