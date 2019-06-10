using BattleOfTheShipsData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class MapTests
	{
		[TestInitialize]
		public void TestSetup()
		{

		}
		[TestMethod]
		public void MapCreatedCorrectly()
		{
			Map m = new Map();
			Assert.IsTrue(m.SetupMap(10, 5));
			Assert.AreEqual<uint>(10, m.MaxX);
			Assert.AreEqual<uint>(5, m.MaxY);
		}
		[TestMethod]
		public void MapCreatedIncorrectlyForNegativeX()
		{
			Map m = new Map();
			Assert.IsFalse(m.SetupMap(-10, 5));
			Assert.AreEqual<uint>(0, m.MaxX);
			Assert.AreEqual<uint>(0, m.MaxY);
		}

		[TestMethod]
		public void MapCreatedIncorrectlyForNegativeY()
		{
			Map m = new Map();
			Assert.IsFalse(m.SetupMap(10, -5));
			Assert.AreEqual<uint>(0, m.MaxX);
			Assert.AreEqual<uint>(0, m.MaxY);
		}

		[TestMethod]
		public void ShipPlacedCorrectly()
		{

		}

		[TestMethod]
		public void MultipleShipsPlacedCorrectly()
		{

		}

		[TestMethod]
		public void ShotCorrectHitNotSank()
		{ }

		[TestMethod]
		public void ShotCorrectHitSank()
		{ }

		[TestMethod]
		public void ShotMiss()
		{ }

		[TestMethod]
		public void ShotOutOfBounds()
		{ }

	}
}
