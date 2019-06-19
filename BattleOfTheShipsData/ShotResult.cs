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
		public bool WasHit { get; set; }
		public bool WasSank { get; set; }

		/// <summary>
		/// Parametrized constructor for setting hit and sank values
		/// </summary>
		/// <param name="wasHit"></param>
		/// <param name="wasSank"></param>
		public ShotResult(bool wasHit, bool wasSank)
		{
			WasHit= wasHit;
			WasSank = wasSank;
		}
		/// <summary>
		/// Default constructor for missed shot
		/// </summary>
		public ShotResult()
		{
			WasHit = false;
			WasSank = false;
		}
	}
}
