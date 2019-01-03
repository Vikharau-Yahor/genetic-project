using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneticLine.Core
{
	public static class Mutator
	{
		private static int YMutationRange;
		private static int XMutationRange;

		public static void Init(int yMutationRange, int xMutationRange)
		{
			YMutationRange = yMutationRange;
			XMutationRange = xMutationRange;
		}

		public static void FillByRandoms(Individual individual)
		{
			var random = new Random();
			for (int i = 0; i < individual.points.Count(); i++)
			{
				individual.points[i].X = random.Next(XMutationRange);
				individual.points[i].Y = random.Next(YMutationRange);
			}
		}
	}
}
