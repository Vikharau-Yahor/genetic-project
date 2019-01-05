using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticLine.Core
{
    public class Gene
    {
        public double Value { get; private set; }
        public double Quality { get; private set; }
        
        public void Update(double value, double quality)
        {
            Value = value;
            Quality = quality;
        }
    }
}
