using GeometryCalculator.Shapes;
using GeometryCalculator.IO;

namespace GeometryCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "ShapeList2.csv";
            string outputFilePath = "ShapeList2_Output.csv";

            ShapeFileReader reader = new ShapeFileReader();
            List<Shape> shapes = reader.ParseShapesFromFile(inputFilePath);

            ShapeFileWriter writer = new ShapeFileWriter();
            writer.WriteShapesToFile(shapes, outputFilePath);

            Console.WriteLine($"Shape properties have been saved to {outputFilePath}");
        }
    }
}
