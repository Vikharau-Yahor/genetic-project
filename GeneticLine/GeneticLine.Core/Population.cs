using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLine.Core
{
	public class Population
	{
		private int _generationNumber;
		private int _pointsCount;
		public IList<Individual> Individuals { get; set; }

		public Population(int populationSize, int pointsCount)
		{
			_generationNumber = 1;
			_pointsCount = pointsCount;
			fillPopulation(populationSize);
		}

		public Individual GetTheBest()
		{
			throw new NotImplementedException();
		}

		private void fillPopulation(int populationSize)
		{
			for (int i = 0; i < populationSize; i++)
			{
				var individual = new Individual(_pointsCount);
				Mutator.FillByRandoms(individual);
				Individuals.Add(individual);
			}
		}


	}
}
