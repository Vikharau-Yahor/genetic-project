using GeneticLine.Core;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GeneticLine.Utils
{
	public static class Extensions
	{
		public static Point GetStartPoint(this Line line)
		{
			return new Point(line.X1, line.Y1);
		}

		public static Line ToLine(this Individual individual, Color color)
		{
			var line = new Line();
			line.X1 = individual.XGene.Value;
			line.Y1 = individual.YGene.Value;
			line.X2 = line.X1 + 1;
			line.Y2 = line.Y1 + 1;
			line.Stroke = new SolidColorBrush(color);
			line.StrokeThickness = 3;
			return line;
		}
	}
}
