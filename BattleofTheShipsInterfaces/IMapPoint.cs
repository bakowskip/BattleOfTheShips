using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleofTheShipsInterfaces
{
	public interface IMapPoint
	{
		uint X { get; set; }
		uint Y { get; set; }
		bool IsShip { get; set; }
		bool WasHit { get; set; }
	}
}
