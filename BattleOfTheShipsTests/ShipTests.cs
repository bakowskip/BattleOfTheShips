using BattleOfTheShipsData;
using BattleofTheShipsInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
		Ship _ship;
		Mock<IMapPoint> shotShipPoint;
		Mock<IMapPoint> okShipPointA;
		Mock<IMapPoint> okShipPointB;

		[TestInitialize]
		public void ShipSetup()
		{
			shotShipPoint = new Mock<IMapPoint>();
			shotShipPoint.Setup(_ => _.IsShip).Returns(true);
			shotShipPoint.Setup(_ => _.WasHit).Returns(true);
			shotShipPoint.Setup(_ => _.IsBlocked).Returns(true);
			shotShipPoint.Setup(_ => _.IsHidden).Returns(false);
			shotShipPoint.Setup(_ => _.X).Returns(0);
			shotShipPoint.Setup(_ => _.Y).Returns(0);

			okShipPointA = new Mock<IMapPoint>();
			okShipPointA.SetupGet(_ => _.IsShip).Returns(true);
			okShipPointA.SetupGet(_ => _.IsBlocked).Returns(true);
			okShipPointA.SetupGet(_ => _.IsHidden).Returns(true);
			okShipPointA.SetupGet(_ => _.X).Returns(0);
			okShipPointA.SetupGet(_ => _.Y).Returns(1);
			okShipPointA.SetupProperty(_ => _.WasHit, false);

			okShipPointB = new Mock<IMapPoint>();
			okShipPointB.SetupGet(_ => _.IsShip).Returns(true);
			okShipPointB.SetupGet(_ => _.IsBlocked).Returns(true);
			okShipPointB.SetupGet(_ => _.IsHidden).Returns(true);
			okShipPointB.SetupGet(_ => _.X).Returns(0);
			okShipPointB.SetupGet(_ => _.Y).Returns(2);
			okShipPointB.SetupProperty(_ => _.WasHit, false);

			var mp = new IMapPoint[3] { shotShipPoint.Object, okShipPointA.Object, okShipPointB.Object };
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
			var mp = new IMapPoint[2] { shotShipPoint.Object, shotShipPoint.Object };
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