using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData
{
    public sealed class MapPoint : IMapPoint
    {
		public int X { get; set; }
		public int Y { get; set; }
		public bool IsShip { get => Ship != null; }
		public bool WasHit { get; set; }
		public bool IsHidden { get; set; }
		public IShip Ship { get; set; }
		public bool IsBlocked { get; set; }

		public MapPoint(int x, int y)
		{
			if (x < 0 || y < 0)
				throw new MapPointException(x, y, $"Invalid coordinate {x},{y}");

			this.X = x;
			this.Y = y;			
		}
    }
}
