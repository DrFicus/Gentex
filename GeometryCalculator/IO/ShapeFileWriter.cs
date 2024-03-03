using System;
using System.Text;
using System.IO;
using GeometryCalculator.Shapes;

namespace GeometryCalculator.IO
{
    public class ShapeFileWriter
    {
        // Constant for specifying the number of decimal points in the output.
        private const int DecimalPoints = 3;

        // Writes a list of shapes to a CSV file.
        public void WriteShapesToFile(List<Shape> shapes, string filePath)
        {
            try
            {
                StringBuilder csvContent = new StringBuilder();
                // Add the header row to the CSV content.
                csvContent.AppendLine("Shape ID,Type,Area,Perimeter,Centroid X,Centroid Y");

                foreach (Shape shape in shapes)
                {
                    try
                    {
                        // Calculate and round the shape's properties.
                        double area = Math.Round(shape.CalculateArea(), DecimalPoints);
                        double perimeter = Math.Round(shape.CalculatePerimeter(), DecimalPoints);
                        var centroid = shape.CalculateCentroid();

                        double centroidX = Math.Round(centroid.X, DecimalPoints);
                        double centroidY = Math.Round(centroid.Y, DecimalPoints);

                        // Add the shape's properties to the CSV content.
                        csvContent.AppendLine($"{shape.Id},{shape.GetType().Name},{area},{perimeter},{centroidX},{centroidY}");
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions related to shape calculations.
                        Console.WriteLine($"Error calculating properties for shape ID {shape.Id}: {ex.Message}");
                    }
                }

                // Write the CSV content to the file.
                File.WriteAllText(filePath, csvContent.ToString());
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle exceptions related to file access permissions.
                Console.WriteLine($"Access to the file path is denied: {filePath}");
            }
            catch (IOException ex)
            {
                // Handle other I/O exceptions.
                Console.WriteLine($"An I/O error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unforeseen exceptions.
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
