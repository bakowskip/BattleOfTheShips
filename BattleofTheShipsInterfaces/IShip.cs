using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleofTheShipsInterfaces
{
	public interface IShip
	{
		IMapPoint[] Area { get; set; }
		bool WasHit { get; }
		bool WasSank { get; }
	}
}