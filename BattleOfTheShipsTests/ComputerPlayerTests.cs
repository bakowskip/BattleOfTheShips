using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleofTheShipsInterfaces;
using Moq;
using BattleOfTheShipsLogic;
using BattleOfTheShipsData.Exceptions;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class ComputerPlayerTests
	{
		private Mock<IMapPoint> _validPointA;
		private Mock<IMapPoint> _validPointB;
		private Mock<IMapPoint> _invalidPoint;
		private Mock<IMapPoint> _shotShipPoint;
		private Mock<IMapPoint> _sankShipPoint;
		private Mock<IMapPoint> _emptyPoint;
		private Mock<IGameMap> _gameMap;
		private Mock<IShip> _hitShip;
		private Mock<IShip> _sankShip;

		private ComputerPlayer _player;

		public ComputerPlayerTests()
		{
			_hitShip = new Mock<IShip>();
			_hitShip.Setup(_ => _.WasHit).Returns(true);
			_hitShip.Setup(_ => _.WasSank).Returns(false);

			_sankShip = new Mock<IShip>();
			_sankShip.Setup(_ => _.WasHit).Returns(true);
			_sankShip.Setup(_ => _.WasSank).Returns(true);

			_shotShipPoint = new Mock<IMapPoint>();
			_shotShipPoint.Setup(_ => _.IsShip).Returns(true);
			_shotShipPoint.Setup(_ => _.WasHit).Returns(true);
			_shotShipPoint.Setup(_ => _.IsBlocked).Returns(true);
			_shotShipPoint.Setup(_ => _.IsHidden).Returns(false);
			_shotShipPoint.Setup(_ => _.Ship).Returns(_hitShip.Object);
			_shotShipPoint.Setup(_ => _.X).Returns(2);
			_shotShipPoint.Setup(_ => _.Y).Returns(1);

			_sankShipPoint = new Mock<IMapPoint>();
			_sankShipPoint.Setup(_ => _.IsShip).Returns(true);
			_sankShipPoint.Setup(_ => _.WasHit).Returns(true);
			_sankShipPoint.Setup(_ => _.IsBlocked).Returns(true);
			_sankShipPoint.Setup(_ => _.IsHidden).Returns(false);
			_sankShipPoint.Setup(_ => _.Ship).Returns(_sankShip.Object);
			_sankShipPoint.Setup(_ => _.X).Returns(1);
			_sankShipPoint.Setup(_ => _.Y).Returns(1);

			_validPointA = new Mock<IMapPoint>();
			_validPointA.Setup(_ => _.X).Returns(3);
			_validPointA.Setup(_ => _.Y).Returns(3);

			_validPointB = new Mock<IMapPoint>();
			_validPointB.Setup(_ => _.X).Returns(3);
			_validPointB.Setup(_ => _.Y).Returns(3);

			_emptyPoint = new Mock<IMapPoint>();
			_emptyPoint.Setup(_ => _.IsShip).Returns(false);
			_emptyPoint.Setup(_ => _.WasHit).Returns(false);
			_emptyPoint.Setup(_ => _.IsBlocked).Returns(false);
			_emptyPoint.Setup(_ => _.IsHidden).Returns(false);
			_emptyPoint.Setup(_ => _.Ship).Returns(() => null);

			_invalidPoint = new Mock<IMapPoint>();
			_invalidPoint.Setup(_ => _.X).Returns(10);
			_invalidPoint.Setup(_ => _.Y).Returns(10);

			_gameMap = new Mock<IGameMap>();
			_gameMap.Setup(_ => _.SetupMap(5, 5)).Returns(true);
			_gameMap.Setup(_ => _.MaxX).Returns(5);
			_gameMap.Setup(_ => _.MaxY).Returns(5);

			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					var _point = new Mock<IMapPoint>();
					_point.Setup(_ => _.IsShip).Returns(false);
					_point.Setup(_ => _.WasHit).Returns(false);
					_point.Setup(_ => _.IsBlocked).Returns(false);
					_point.Setup(_ => _.IsHidden).Returns(false);
					_point.Setup(_ => _.Ship).Returns(() => null);
					_point.Setup(_ => _.X).Returns(i);
					_point.Setup(_ => _.Y).Returns(j);
					_gameMap.Setup(_ => _[i, j]).Returns(_point.Object);
				}
			}

			_gameMap.Setup(_ => _[1, 2]).Returns(_validPointB.Object);
			_gameMap.Setup(_ => _[2, 0]).Returns(_validPointB.Object);
			_gameMap.Setup(_ => _[3, 1]).Returns(_validPointB.Object);
			_gameMap.Setup(_ => _[1, 1]).Returns(_sankShipPoint.Object);
			_gameMap.Setup(_ => _[2, 1]).Returns(_shotShipPoint.Object);

			_player = new ComputerPlayer(_gameMap.Object);
		}

		[TestMethod]
		public void ComputerPlayerCreatedSuccessfullyForGoodMap()
		{
			_validPointA = new Mock<IMapPoint>();
			_validPointA.Setup(p => p.X).Returns(0);
			_validPointA.Setup(p => p.Y).Returns(0);
			_validPointB = new Mock<IMapPoint>();
			_validPointB.Setup(p => p.X).Returns(1);
			_validPointB.Setup(p => p.Y).Returns(0);
			_gameMap = new Mock<IGameMap>();
			_gameMap.Setup(m => m.SetupMap(2, 1)).Returns(true);
			_gameMap.Setup(m => m.MaxX).Returns(2);
			_gameMap.Setup(m => m.MaxY).Returns(1);
			_gameMap.Setup(m => m.MapArea).Returns(new IMapPoint[,] { { _validPointA.Object, _validPointB.Object } });

			_player = new ComputerPlayer(_gameMap.Object);
			Assert.IsNotNull(_player);

		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException),"GameMap = null was allowed")]
		public void ComputerPlayerThrowsExceptionForNullMap()
		{
			 _player = new ComputerPlayer(null);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "GameMap = null was allowed")]
		public void ComputerPlayerThrowsExceptionForEmptyMap()
		{
			IMapPoint[,] emptyMap = null;
			_gameMap = new Mock<IGameMap>();
			_gameMap.Setup(m => m.MaxX).Returns(0);
			_gameMap.Setup(m => m.MaxY).Returns(0);
			_gameMap.Setup(m => m.MapArea).Returns(emptyMap);
			_player = new ComputerPlayer(null);
		}


		[TestMethod]
		public void ComputerPlayerShipsPlacedCorrectly()
		{
			var gameMap = new Mock<IGameMap>();
			gameMap.Setup(_ => _.SetupMap(10, 10)).Returns(true);
			gameMap.Setup(_ => _.MaxX).Returns(10);
			gameMap.Setup(_ => _.MaxY).Returns(10);

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					var point = new Mock<IMapPoint>();
					point.Setup(_ => _.IsShip).Returns(false);
					point.Setup(_ => _.WasHit).Returns(false);
					point.Setup(_ => _.IsBlocked).Returns(false);
					point.Setup(_ => _.IsHidden).Returns(false);
					point.Setup(_ => _.Ship).Returns(() => null);
					point.Setup(_ => _.X).Returns(i);
					point.Setup(_ => _.Y).Returns(j);
					gameMap.Setup(_ => _[i, j]).Returns(point.Object);
				}
			}

			var player = new ComputerPlayer(gameMap.Object);
			player.PlaceShips(3, 2);

			Assert.AreEqual(3, player.Ships.Count);
		}

		[TestMethod]
		public void ComputerPlayerTooBigShipThrowsException()
		{
			_validPointA = new Mock<IMapPoint>();
			_validPointA.Setup(p => p.X).Returns(0);
			_validPointA.Setup(p => p.Y).Returns(0);
			_validPointB = new Mock<IMapPoint>();
			_validPointB.Setup(p => p.X).Returns(1);
			_validPointB.Setup(p => p.Y).Returns(0);
			_gameMap = new Mock<IGameMap>();
			_gameMap.Setup(m => m.SetupMap(2, 1)).Returns(true);
			_gameMap.Setup(m => m.MaxX).Returns(2);
			_gameMap.Setup(m => m.MaxY).Returns(1);
			_gameMap.Setup(m => m.MapArea).Returns(new IMapPoint[,] { { _validPointA.Object, _validPointB.Object } });

			_player = new ComputerPlayer(_gameMap.Object);
			Assert.IsNotNull(_player);

			Assert.ThrowsException<ShipException>(() => _player.PlaceShips(2, 1));
		}

		[TestMethod]
		public void CorrectlyReportsEndOfGameForAllShipSank()
		{
			Mock<IShip> shipA = new Mock<IShip>();
			Mock<IShip> shipB = new Mock<IShip>();
			shipA.Setup(s => s.WasHit).Returns(true);
			shipA.Setup(s => s.WasSank).Returns(true);
			shipB.Setup(s => s.WasHit).Returns(true);
			shipB.Setup(s => s.WasSank).Returns(true);

			_player.Ships = new IShip[] {shipA.Object, shipB.Object};

			Assert.IsTrue(_player.IsGameOver);
		}

		[TestMethod]
		public void CorrectlyReportsEndOfGameForNotAllShipSank()
		{
			Mock<IShip> shipA = new Mock<IShip>();
			Mock<IShip> shipB = new Mock<IShip>();
			shipA.Setup(s => s.WasHit).Returns(true);
			shipA.Setup(s => s.WasSank).Returns(true);
			shipB.Setup(s => s.WasHit).Returns(true);
			shipB.Setup(s => s.WasSank).Returns(false);

			_player.Ships = new IShip[] { shipA.Object, shipB.Object };

			Assert.IsFalse(_player.IsGameOver);
		}

		[TestMethod]
		public void ShotCorrectHitNotSank()
		{
			var sr = _player.CheckShot(_shotShipPoint.Object);

			Assert.IsNotNull(sr);
			Assert.IsTrue(sr.WasHit);
			Assert.IsFalse(sr.WasSank);
		}

		[TestMethod]
		public void ShotCorrectHitSank()
		{
			var sr = _player.CheckShot(_sankShipPoint.Object);

			Assert.IsNotNull(sr);
			Assert.IsTrue(sr.WasHit);
			Assert.IsTrue(sr.WasSank);
		}

		[TestMethod]
		public void ShotMiss()
		{
			var sr = _player.CheckShot(_sankShipPoint.Object);

			Assert.IsNotNull(sr);
			Assert.IsTrue(sr.WasHit);
			Assert.IsTrue(sr.WasSank);
		}

		[TestMethod]
		public void ShotOutOfBounds()
		{
			Assert.ThrowsException<MapPointException> (() => _player.CheckShot(_invalidPoint.Object));
		}
	}
}
