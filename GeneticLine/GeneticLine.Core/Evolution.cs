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
		private Point[] _goal;
		private bool evolutionStopped;
		private int evolutionSpeed;

		//evolution laws
		private int _populationSize;
		private int _individualPointsCount;

		public Evolution(Point[] goal, int populationSize, int individualPointsCount)
		{
			_goal = goal;
			_populationSize = populationSize;
			_individualPointsCount = individualPointsCount;

		}

		public void RunEvolutionCycle()
		{
			// create population with random individuals
			var population = new Population(_populationSize, _individualPointsCount);
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
