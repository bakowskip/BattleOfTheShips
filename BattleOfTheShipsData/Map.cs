using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData
{
	public class Map : IGameMap
	{
		private IMapPoint[,] _mapPoints;
		private IList<IShip> _ships;

		public int MaxX { get => (int)(_mapPoints?.GetLength(0) ?? 0); }
		public int MaxY { get => (int)(_mapPoints?.GetLength(1) ?? 0); }
		public IMapPoint[,] MapArea { get => _mapPoints; }

		public IEnumerable<IShip> Ships { get => _ships; }

		public bool SetupMap(int sizeX, int sizeY)
		{
			if (sizeX > 0 && sizeY > 0)
			{
				_ships = new List<IShip>();
				_mapPoints = new MapPoint[sizeX, sizeY];
				for (int x = 0; x < sizeX; x++)
					for (int y = 0; y < sizeY; y++)
					{
						_mapPoints[x, y] = new MapPoint(x, y);
						_mapPoints[x, y].IsHidden = true;
					}
				return true;
			}
			return false;
		}

		//FIX IT for both directions
		public IShip PlaceShip(IMapPoint[] shipArea)
		{
			//mark marigin
			foreach(IMapPoint mp in shipArea)
			{
				for (int x = -1; x < 2; x++)
				{
					for (int y = -1; y < 2; y++)
						MapArea[mp.X + x, mp.Y + y].IsBlocked = true;
				}
				
			}
			var newShip = new Ship(shipArea);
			_ships.Add(newShip);
			return newShip;
		}

		public bool PlaceShot(int x, int y)
		{
			if (x >= _mapPoints.GetLength(0) || y >= _mapPoints.GetLength(1) || x <0 || y < 0)
				throw new InvalidShotException(x, y);

			return _mapPoints[x, y].IsShip;
		}
	}
}
