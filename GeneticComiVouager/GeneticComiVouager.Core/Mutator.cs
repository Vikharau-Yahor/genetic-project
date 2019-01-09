using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace GeneticComiVouager.Core
{
	public static class Mutator
	{
		private static int YMutationRange;
		private static int XMutationRange;
		private static Random random = new Random();

		public static void Init(int yMutationRange, int xMutationRange)
		{
			YMutationRange = yMutationRange;
			XMutationRange = xMutationRange;
		}

		public static void InitialMutate(Individual individual, MutatationData[] mutations)
		{
			var maxNumber = mutations.OrderByDescending(x => x.Number).First().Number;
			var geneValue = new List<int>();

			while (geneValue.Count != mutations.Length - 1)
			{
				var randomValue = random.Next(1, mutations.Length);
				if (!geneValue.Contains(randomValue))
					geneValue.Add(randomValue);
			}
			
            var geneQuality = getQuality(geneValue, mutations);
            individual.Gene.Update(geneValue.ToArray(), geneQuality);
        }

		public static void Mutate(Individual individual, MutatationData[] mutations)
		{
			var randomFirstPosition = random.Next(individual.Gene.Value.Length);
			var randomSecondPosition = random.Next(individual.Gene.Value.Length);

			while (randomSecondPosition == randomFirstPosition)
			{
				randomSecondPosition = random.Next(individual.Gene.Value.Length);
			}
			var geneNewValue = individual.Gene.Value;
			var buffer = individual.Gene.Value[randomFirstPosition];
			geneNewValue[randomFirstPosition] = geneNewValue[randomSecondPosition];
			geneNewValue[randomSecondPosition] = buffer;
			var geneQuality = getQuality(geneNewValue, mutations);
			individual.Gene.Update(geneNewValue, geneQuality);
		}

		private static double getQuality(IList<int> geneValue, MutatationData[] mutations)
        {
			var startPosition = mutations[0];
			double quality = 0;
			for (int i = 0; i < geneValue.Count; i++)
			{
				var endPosition = mutations.First(x => x.Number == geneValue[i]);
				quality -= getRangeBetweenCities(startPosition, endPosition);
				startPosition = endPosition;
			}

			quality -= getRangeBetweenCities(startPosition, mutations[0]);

			return quality;

		}

		private static double getRangeBetweenCities(MutatationData firstMutation, MutatationData secondMutation)
		{
			var xRange = Math.Abs(firstMutation.X - secondMutation.X);
			var yRange = Math.Abs(firstMutation.Y - secondMutation.Y);
			return Math.Sqrt((xRange * xRange) + (yRange * yRange));
		}
    }
}
