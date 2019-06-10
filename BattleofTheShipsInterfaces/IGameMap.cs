using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleofTheShipsInterfaces
{
    public interface IGameMap
    {
		bool SetupMap(int sizeX, int sizeY);
		bool PlaceShip(IMapPoint startingPoint, ushort size, char direction);
		IShotResult PlaceShot(IMapPoint target);
		uint MaxX { get; }
		uint MaxY { get; }
	}
}
