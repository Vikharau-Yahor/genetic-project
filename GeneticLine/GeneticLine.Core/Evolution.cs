using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GeneticLine.Core
{
	public class Evolution
	{
		private Point[] _goals;
        private const double DeathRate = 0.4;
		private bool evolutionStopped;
		private int evolutionSpeed;

		//evolution laws
		private int _populationSize;
		private int _individualPointsCount;

		public Evolution(Point[] goals, int populationSize, int individualPointsCount)
		{
            _goals = goals;
			_populationSize = populationSize;
			_individualPointsCount = individualPointsCount;

		}

		public void RunEvolutionCycle()
		{
            // create populations
            var populations = new List<Population>();
            foreach (var goal in _goals)
            {
                var xGene = new Gene();
                var yGene = new Gene();
                xGene.Update(goal.X, 100);
                yGene.Update(goal.Y, 100);
                populations.Add(new Population(_populationSize, DeathRate, xGene, yGene));
            }

			while (!evolutionStopped)
			{
				//Evolute(population);
				// population evolute
				// get best individual and render it
				// * evaluate survival rate for every individual + mutation chance for chomosomes
				// * calculate survivalDieRate, kill  individuals that have low rate
				// * create new individuals from survived parents and mutate chromosomes for all (??)
				Thread.Sleep(evolutionSpeed);
			}
		}
				
	}
}
