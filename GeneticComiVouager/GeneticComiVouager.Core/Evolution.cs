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
		private bool _evolutionStopped = false;
		private int _evolutionSpeed = 250;
		private List<Task> _lifeCycleTasks = new List<Task>();
		//evolution laws
		private int _populationSize;

		public List<Population> Populations { get; private set; }

		public Evolution(Point[] goals, int populationSize)
		{
			_goals = goals;
			_populationSize = populationSize;
			Populations = new List<Population>();
			var id = 1;

			foreach (var goal in _goals)
			{
				var xGene = new Gene();
				var yGene = new Gene();
				xGene.Update(goal.X, 100);
				yGene.Update(goal.Y, 100);
				Populations.Add(new Population(id, _populationSize, DeathRate, xGene, yGene));
				id++;
			}
		}

		public void RunEvolutionCycle()
		{
			// todo for all populations
			//var population = Populations.First();
			foreach (var population in Populations)
			{
				_lifeCycleTasks.Add(new Task(() => runEvolutionCycle(population)));
			}

			foreach (var task in _lifeCycleTasks)
			{
				task.Start();
			}
		}

		private void runEvolutionCycle(Population population)
		{
			while (!_evolutionStopped)
			{
				population.ExecuteLifeCycle();
				Thread.Sleep(_evolutionSpeed);
			}
		}

		public void Stop()
		{
			_evolutionStopped = true;
			Task.WaitAll(_lifeCycleTasks.ToArray());
			_lifeCycleTasks.Clear();
		}
				
	}
}
