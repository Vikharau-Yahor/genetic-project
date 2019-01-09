using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticComiVouager.Core
{
	public class Population
	{
		private MutatationData[] _mutations;
		private int _killsNumberPerGeneration;

        public int GenerationNumber { get; private set; }
		public IList<Individual> Individuals { get; set; }
		public Individual BestIndividual { get; private set; }

		public event Action<Population> OnPopulationLifecycleEnd;

		public Population(int populationSize, double deathRate, MutatationData[] mutations)
		{
			GenerationNumber = 1;
            _killsNumberPerGeneration = (int)(populationSize * deathRate);
			_mutations = mutations;
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
						Mutator.Mutate(individual, _mutations);
						Mutator.Mutate(individual, _mutations);
						break;
					case LifeStatus.AdultLife:
						Mutator.Mutate(individual, _mutations);
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
            individual.SurvivalRate = individual.Gene.Quality;
        }

		private void fillPopulation(int populationSize)
		{
			for (int i = 0; i < populationSize; i++)
			{
				var individual = new Individual();
				Mutator.InitialMutate(individual, _mutations);
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
