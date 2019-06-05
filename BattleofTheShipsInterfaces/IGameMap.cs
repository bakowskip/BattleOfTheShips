using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleofTheShipsInterfaces
{
    public interface IGameMap
    {
		void SetupMap(int sizeX, int sizeY);
		void PlaceShip(IMapPoint startingPoint, uint size, char direction);
		void PlaceShot(IMapPoint target);
    }
}
