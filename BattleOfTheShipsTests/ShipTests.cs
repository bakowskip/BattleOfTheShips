using BattleOfTheShipsData;
using BattleofTheShipsInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class ShipTests
	{
		private IShip _ship;

		[TestInitialize]
		public void ShipSetup()
		{
			var mp = new MapPoint[] { new MapPoint(1, 1) { WasHit = true }, new MapPoint(1, 2), new MapPoint(1, 3) };
			_ship = new Ship(mp);
			
		}

		[TestMethod]
		public void ShipCorrectlyReportedAsHitNotSank()
		{
			Assert.IsTrue(_ship.WasHit);
			Assert.IsFalse(_ship.WasSank);
		}

		[TestMethod]
		public void ShipCorrectlyReportedAsSank()
		{
			var mp = new MapPoint[] { new MapPoint(1, 1) { WasHit = true } };
			_ship = new Ship(mp);

			Assert.IsTrue(_ship.WasHit);
			Assert.IsTrue(_ship.WasSank);

		}

		[TestMethod]
		public void ShipCorrectlyReportedAsHitNotSankAfterNewHit()
		{
			_ship.Area[1].WasHit = true;

			Assert.IsTrue(_ship.WasHit);
			Assert.IsFalse(_ship.WasSank);
		}

		[TestMethod]
		public void ShipCorrectlyReportedAsHitAndSankAfterLastHit()
		{
			_ship.Area[1].WasHit = true;
			_ship.Area[2].WasHit = true;

			Assert.IsTrue(_ship.WasHit);
			Assert.IsTrue(_ship.WasSank);
		}
	}
}