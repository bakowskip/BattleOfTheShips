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
		IShip PlaceShip(IMapPoint[] shipArea);
		int MaxX { get; }
		int MaxY { get; }
		IMapPoint[,] MapArea { get; }
		IEnumerable<IShip> Ships { get; }
	}
}