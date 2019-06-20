using System.Collections.Generic;

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

		IMapPoint this[int x,int y] { get; }
	}
}