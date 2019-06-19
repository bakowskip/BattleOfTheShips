using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleofTheShipsInterfaces
{
	public interface IPresenter
	{
		void ShowMap(IGameMap map);
		void ShowHitResult(IMapPoint target, bool wasHit, bool wasSank);
	}
}
