using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticComiVouager.Core
{
    public class Gene
    {
        public int[] Value { get; private set; }
        public double Quality { get; private set; }

		public Gene()
		{

		}

		public Gene(Gene inheritedGene)
		{
			Value = new int[inheritedGene.Value.Length];

			for (int i = 0; i < inheritedGene.Value.Length; i++)
			{
				Value[i] = inheritedGene.Value[i];
			}

			Quality = inheritedGene.Quality;
		}

        public void Update(int[] value, double quality)
        {
            Value = value;
            Quality = quality;
        }
    }
}
