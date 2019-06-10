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
		public uint X { get; set; }
		public uint Y { get; set; }
		public bool IsShip { get; set; }
		public bool WasHit { get; set; }
		public bool IsHidden { get; set; }

		public MapPoint(int x, int y)
		{
			if (x < 1 || y < 1)
				throw new MapPointException(x, y, $"Invalid coordinate {x},{y}");

			this.X = (uint)x;
			this.Y = (uint)y;
		}

		public MapPoint(int x, int y, bool isShip) : this(x,y)
		{
			this.IsShip = isShip;
		}
    }
}
