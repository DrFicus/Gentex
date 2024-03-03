namespace GeometryCalculator.Shapes
{
    public class Circle : Shape
    {
        public Circle(int id) : base(id) { }

        public override double CalculateArea()
        {
            double radius = Properties["Radius"];
            return Math.PI * radius * radius;
        }

        public override double CalculatePerimeter()
        {
            double radius = Properties["Radius"];
            return 2 * Math.PI * radius;
        }

        public override (double X, double Y) CalculateCentroid()
        {
            return (Properties["CenterX"], Properties["CenterY"]);
        }

        public override void ValidateProperties(int lineNumber)
        {
            if (!Properties.ContainsKey("Radius") || !Properties.ContainsKey("CenterX") || !Properties.ContainsKey("CenterY"))
            {
                throw new InvalidOperationException("Circle is missing required properties.");
            }
        }
    }
}
