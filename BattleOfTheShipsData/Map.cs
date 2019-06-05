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

		public void SetupMap(int sizeX, int sizeY)
		{
			//mapPoints = new MapPoint[sizeX, sizeY];
			return;
		}

		public void PlaceShip(IMapPoint startingPoint, uint size, char direction)
		{
			return;
		}
		public void PlaceShot(IMapPoint target)
		{
			return;
		}
	}
}
