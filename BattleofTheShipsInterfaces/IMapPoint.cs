using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleofTheShipsInterfaces
{
	public interface IMapPoint
	{
		int X { get; set; }
		int Y { get; set; }
		bool IsShip { get; }
		bool WasHit { get; set; }
		bool IsHidden { get; set; }
		IShip Ship { get; set; }
		bool IsBlocked { get; set; }
	}
}
