using GeneticComiVouager.Core;
using GeneticComiVouager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticComiVouager.Mappers
{
	public static class Mapper
	{
		public static MutatationData ToMutationData(this CityVm city)
		{
			return new MutatationData
			{
				Number = city.Number,
				X = city.Position.X,
				Y = city.Position.Y
			};
		}
	}
}
