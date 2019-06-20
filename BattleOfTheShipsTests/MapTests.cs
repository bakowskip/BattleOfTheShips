using BattleOfTheShipsData;
using BattleofTheShipsInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class MapTests
	{
		Mock<IMapPoint>[] mps;
		Map map;
		[TestInitialize]
		public void TestSetup()
		{
			map = new Map();

			mps = new Mock<IMapPoint>[4];
			for (int i = 0; i < 4; i++)
			{
				mps[i] = new Mock<IMapPoint>();
				mps[i].Setup(mp => mp.X).Returns(i + 1);
				mps[i].Setup(mp => mp.Y).Returns(4);
				mps[i].Setup(mp => mp.WasHit).Returns(false);
				mps[i].Setup(mp => mp.IsShip).Returns(false);
				mps[i].Setup(mp => mp.IsBlocked).Returns(false);
			}			
		}

		[TestMethod]
		public void MapCreatedCorrectly()
		{			
			Assert.IsTrue(map.SetupMap(10, 5));
			Assert.AreEqual(10, map.MaxX);
			Assert.AreEqual(5, map.MaxY);
		}
		[TestMethod]
		public void MapCreatedIncorrectlyForZeroX()
		{
			Assert.IsFalse(map.SetupMap(0, 5));
			Assert.AreEqual(0, map.MaxX);
			Assert.AreEqual(0, map.MaxY);
		}

		[TestMethod]
		public void MapCreatedIncorrectlyForZeroY()
		{
			Assert.IsFalse(map.SetupMap(10, 0));
			Assert.AreEqual(0, map.MaxX);
			Assert.AreEqual(0, map.MaxY);
		}

		[TestMethod]
		public void ShipPlacedCorrectly()
		{
			Assert.IsTrue(map.SetupMap(10, 10));

			var ship = map.PlaceShip(new IMapPoint[] { mps[0].Object, mps[1].Object, mps[2].Object, mps[3].Object });
			Assert.IsNotNull(ship);
			Assert.IsInstanceOfType(ship, typeof(IShip));
			Assert.IsNotNull(ship.Area);
			Assert.AreEqual(4, ship.Area.Length);
			Assert.IsFalse(ship.WasHit);
			Assert.IsFalse(ship.WasSank);
		}
	}
}
