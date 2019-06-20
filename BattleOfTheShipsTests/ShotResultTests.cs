using System;
using BattleOfTheShipsData;
using BattleOfTheShipsData.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class ShotResultTests
	{
		[TestMethod]
		public void ShotResultHitNotSank()
		{
			var sr = new ShotResult(true, false);
			Assert.IsFalse(sr.WasSank);
			Assert.IsTrue(sr.WasHit);
		}

		[TestMethod]
		public void ShotResultMiss()
		{
			var sr = new ShotResult(false, false);
			Assert.IsFalse(sr.WasSank);
			Assert.IsFalse(sr.WasHit);
		}

		[TestMethod]
		public void ShotResultHitAndSank()
		{
			var sr = new ShotResult(true, true);
			Assert.IsTrue(sr.WasSank);
			Assert.IsTrue(sr.WasHit);
		}

		[TestMethod]
		public void ShotResultMissAndSankThrowsException()
		{
			Assert.ThrowsException<InvalidShotResultException>(() => new ShotResult(false, true));
			
		}




	}
}
