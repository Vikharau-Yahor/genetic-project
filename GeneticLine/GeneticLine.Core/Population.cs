using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLine.Core
{
	public class Population
	{
        private Gene _xGoal;
        private Gene _yGoal;
        private int _killsNumberPerGeneration;

        public int GenerationNumber { get; private set; }
		public IList<Individual> Individuals { get; set; }

		public Population(int populationSize, double deathRate, Gene xGoal, Gene yGoal)
		{
			GenerationNumber = 1;
            _killsNumberPerGeneration = (int)(populationSize * deathRate);
            _xGoal = xGoal;
            _yGoal = yGoal;

            fillPopulation(populationSize);
		}

        public void ExecuteLifeCycle()
        {
            foreach(var individual in Individuals)
            {
                updateSurvivalRate(individual);
            }

            var individualsToKill = Individuals.OrderBy(x => x.SurvivalRate).Take(_killsNumberPerGeneration);

            foreach (var individual in individualsToKill)
            {
                individual.Kill();
            }

            GenerationNumber++;
        }

		public Individual GetTheBest()
		{
            return Individuals.OrderByDescending(x => x.SurvivalRate).First();
        }

        private void updateSurvivalRate(Individual individual)
        {
            var xGeneQuality = individual.XGene.Quality;
            var yGeneQuality = individual.YGene.Quality;
            individual.SurvivalRate = (xGeneQuality + yGeneQuality) / 2;
        }

		private void fillPopulation(int populationSize)
		{
			for (int i = 0; i < populationSize; i++)
			{
				var individual = new Individual();
				Mutator.InitialMutate(individual, _xGoal, _yGoal);
				Individuals.Add(individual);
			}
		}


	}
}
