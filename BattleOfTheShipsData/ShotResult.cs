using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsData
{
	public class ShotResult
	{
		private bool _hit;
		private bool _sank;
		public bool WasHit { get => _hit; }
		public bool WasSank { get => _sank; }

		/// <summary>
		/// Parametrized constructor for setting hit and sank values
		/// </summary>
		/// <param name="wasHit"></param>
		/// <param name="wasSank"></param>
		public ShotResult(bool wasHit, bool wasSank)
		{
			if (!wasHit && wasSank)
				throw new InvalidShotResultException();

			_hit = wasHit;
			_sank = wasSank;
		}
		/// <summary>
		/// Default constructor for missed shot
		/// </summary>
		public ShotResult()
		{
			_hit = false;
			_sank = false;
		}
	}
}
