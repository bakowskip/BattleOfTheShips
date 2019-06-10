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
		private MapPoint[,] mapPoints;

		public uint MaxX { get => (uint)(mapPoints?.GetLength(0) ?? 0); }
		public uint MaxY { get => (uint)(mapPoints?.GetLength(1) ?? 0); }

		public bool SetupMap(int sizeX, int sizeY)
		{
			if (sizeX > 0 && sizeY > 0)
			{
				mapPoints = new MapPoint[sizeX, sizeY];
				return true;
			}
			return false;
		}

		public bool PlaceShip(IMapPoint startingPoint, ushort size, char direction)
		{

			return false;
		}
		public IShotResult PlaceShot(IMapPoint target)
		{
			if (mapPoints[target.X, target.Y].IsShip)
			{

			}
			else
				return new ShotResult();
			return null;
		}
	}
}
