using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GeneticComiVouager.Core
{
	public class Evolution
	{
		private const double DeathRate = 0.4;
		private bool _evolutionStopped = false;
		private Task _lifeCycleTask;
		private int _evolutionSpeed;

		//evolution laws
		private int _populationSize;

		public Population Population { get; private set; }

		public Evolution(MutatationData[] mutations, int populationSize, int evolutionSpeed)
		{
			_evolutionSpeed = evolutionSpeed;
			_populationSize = populationSize;
			Population = new Population(_populationSize, DeathRate, mutations);
		}

		public void RunEvolutionCycle()
		{
			_lifeCycleTask = new Task(() => runEvolutionCycle(Population));
			_lifeCycleTask.Start();	
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
			_lifeCycleTask.Wait();
		}
				
	}
}
