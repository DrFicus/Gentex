using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using GeometryCalculator.Shapes;

namespace GeometryCalculator.IO
{
    public class ShapeFileReader
    {
        // A dictionary mapping shape types to their constructors.
        private Dictionary<string, Func<int, Shape>> shapeFactory = new Dictionary<string, Func<int, Shape>>
        {
            { "Circle", id => new Circle(id) },
            { "Equilateral Triangle", id => new EquilateralTriangle(id) },
            { "Ellipse", id => new Ellipse(id) },
            { "Square", id => new Square(id) },
            { "Polygon", id => new Polygon(id) }
        };

        // Parses shapes from a file and returns a list of Shape objects.
        public List<Shape> ParseShapesFromFile(string filePath)
        {
            List<Shape> shapes = new List<Shape>();

            using (var reader = new StreamReader(filePath))
            {
                int lineNumber = 0;
                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Check if the line has at least 3 elements (ID, shape type, and at least one property).
                    if (values.Length < 3)
                    {
                        throw new FormatException($"Line {lineNumber}: Insufficient data.");
                    }

                    // Check if the first element is a valid integer and the second element is not empty.
                    if (!int.TryParse(values[0], out int id) || string.IsNullOrEmpty(values[1]))
                    {
                        throw new FormatException($"Line {lineNumber}: Invalid ID or shape type.");
                    }

                    // Check if the shape type is supported.
                    if (!shapeFactory.TryGetValue(values[1], out var shapeConstructor))
                    {
                        throw new FormatException($"Line {lineNumber}: Unsupported shape type '{values[1]}'.");
                    }

                    // Create the shape and parse its properties.
                    var shape = shapeConstructor(id);
                    ParseShapeProperties(shape, values, lineNumber);
                    shape.ValidateProperties(lineNumber);
                    shapes.Add(shape);
                }
            }

            return shapes;
        }

        // Parses the properties of a shape from a line of the file.
        private void ParseShapeProperties(Shape shape, string[] values, int lineNumber)
        {
            for (int i = 2; i < values.Length; i += 2)
            {
                // Check if there are at least two elements left for a property name and value.
                if (i + 1 >= values.Length || string.IsNullOrEmpty(values[i]) || string.IsNullOrEmpty(values[i + 1]))
                {
                    break;
                }

                // Check if the property value is a valid double.
                if (!double.TryParse(values[i + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                {
                    throw new FormatException($"Line {lineNumber}: Invalid value for property '{values[i]}'.");
                }

                // Add the property to the shape.
                shape.Properties.Add(values[i], value);
            }
        }
    }
}
