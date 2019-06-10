using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData
{
	public class Ship : IShip
	{
		public IMapPoint[] Area { get; set; }

		public bool WasHit => (Area != null && Area.Any(mp => mp.WasHit));

		public bool WasSank => (Area != null && Area.All(mp => mp.WasHit));

		public Ship(IMapPoint[] shipArea)
		{
			Area = shipArea;
		}
	}
}
