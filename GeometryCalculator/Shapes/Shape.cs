namespace GeometryCalculator.Shapes
{
    public interface IShape
    {
        double CalculateArea();
        double CalculatePerimeter();
        (double X, double Y) CalculateCentroid();
        void ValidateProperties(int lineNumber);
    }

    public abstract class Shape : IShape
    {
        public int Id { get; set; }
        public Dictionary<string, double> Properties { get; set; }

        protected Shape(int id)
        {
            Id = id;
            Properties = new Dictionary<string, double>();
        }

        public abstract double CalculateArea();
        public abstract double CalculatePerimeter();
        public abstract (double X, double Y) CalculateCentroid();
        public abstract void ValidateProperties(int lineNumber);
    }
}
