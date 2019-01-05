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

		public static void InitialMutate(Individual individual, Gene xGoal, Gene yGoal)
		{
			var random = new Random();

            var xValue = random.Next(XMutationRange); ;
            var xQuality = getQuality(xValue, xGoal.Value);
            individual.XGene.Update(xValue, xQuality);

            var yValue = random.Next(YMutationRange); ;
            var yQuality = getQuality(yValue, yGoal.Value);
            individual.YGene.Update(yValue, yQuality);
        }

        private static double getQuality(double geneValue, double perfectValue)
        {
            return (Math.Abs(perfectValue - geneValue) * -1) + 100;
        }
    }
}
