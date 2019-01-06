using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticLine.Core
{
	public class Population
	{
        private Gene _xGoal;
        private Gene _yGoal;
        private int _killsNumberPerGeneration;

		public readonly int Id;
        public int GenerationNumber { get; private set; }
		public IList<Individual> Individuals { get; set; }
		public Individual BestIndividual { get; private set; }

		public event Action<Population> OnPopulationLifecycleEnd;

		public Population(int id, int populationSize, double deathRate, Gene xGoal, Gene yGoal)
		{
			Id = id;
			GenerationNumber = 1;
            _killsNumberPerGeneration = (int)(populationSize * deathRate);
            _xGoal = xGoal;
            _yGoal = yGoal;
			Individuals = new List<Individual>();

			fillPopulation(populationSize);
		}

        public void ExecuteLifeCycle()
        {
			born();
			live();
			death();

            GenerationNumber++;

			OnPopulationLifecycleEnd?.Invoke(this);
		}

		private void born()
		{
			var deadIndividuals = Individuals.Where(x => x.LifeStatus == LifeStatus.Dead).ToList();
			if (deadIndividuals.Count == 0) return;

			var parents = Individuals.Where(x => x.LifeStatus == LifeStatus.AdultLife).ToList();
			var parentsCount = parents.Count;
			var randomSelector = new Random();

			foreach (var dead in deadIndividuals)
			{
				getParents(parents, parentsCount, randomSelector, out var firstParent, out var secondParent);
				dead.Reborn(firstParent, secondParent);
			}
		}

		private void live()
		{
			foreach (var individual in Individuals)
			{
				if (individual == BestIndividual) {
					individual.Rest();
					continue;
				}

				switch(individual.LifeStatus)
				{
					case LifeStatus.Childhood:
						individual.Grow();
						break;
					case LifeStatus.AdultLife:
						Mutator.Mutate(individual, _xGoal, _yGoal);
						break;
					default: throw new Exception("Incorrect life status (dead individual can't do any actions)");
				}

				updateSurvivalRate(individual);
			}

			BestIndividual = Individuals.OrderByDescending(x => x.SurvivalRate).First();
		}

		private void death()
		{
			var individualsToKill = Individuals.OrderBy(x => x.SurvivalRate).Take(_killsNumberPerGeneration).ToList();

			foreach (var individual in individualsToKill)
			{
				individual.Kill();
			}
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

		private void getParents(List<Individual> parents, int parentsCount, Random randomSelector, out Individual firstParent, out Individual secondParent)
		{
			firstParent = parents[randomSelector.Next(parentsCount)];
			secondParent = parents[randomSelector.Next(parentsCount)];

			if (firstParent == secondParent)
				getParents(parents, parentsCount, randomSelector, out firstParent, out secondParent);
		}
	}
}
