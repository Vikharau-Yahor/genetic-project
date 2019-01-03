using System.Windows;
using System.Windows.Shapes;

namespace GeneticLine.Utils
{
	public static class LineExtensions
	{
		public static Point GetStartPoint(this Line line)
		{
			return new Point(line.X1, line.Y1);
		}
	}
}
