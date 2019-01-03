using System.Windows;

namespace GeneticLine.Core
{
    public class Individual
    {
		public Point[] points;

		public Individual(int pointsCount)
		{
			points = new Point[pointsCount];
		}
    }
}
