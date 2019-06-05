using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleOfTheShipsData;
using BattleOfTheShipsData.Exceptions;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class MapPointTests
	{
		[TestMethod]
		public void MapPointCreatedCorrectly()
		{
			var mp = new MapPoint(1, 2);
			Assert.IsTrue(mp != null);
		}

		[TestMethod]
		public void MapPointThrowsExceptionForInvalidX()
		{
			try
			{
				var mp = new MapPoint(-1, 5);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapPointException));
				var mpEx = ex as MapPointException;
				Assert.AreEqual(-1, mpEx.X);
				Assert.AreEqual(5, mpEx.Y);
			}
		}

		[TestMethod]
		public void MapPointThrowsExceptionForInvalidY()
		{
			try
			{
				var mp = new MapPoint(2, -5);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapPointException));
				var mpEx = ex as MapPointException;
				Assert.AreEqual(2, mpEx.X);
				Assert.AreEqual(-5, mpEx.Y);
			}
		}

		[TestMethod]
		public void MapPointThrowsExceptionForInvalidXandY()
		{
			try
			{
				var mp = new MapPoint(-100, -500);
			}
			catch (Exception ex)
			{
				Assert.IsInstanceOfType(ex, typeof(MapPointException));
				var mpEx = ex as MapPointException;
				Assert.AreEqual(-100, mpEx.X);
				Assert.AreEqual(-500, mpEx.Y);
			}
		}
	}
}
