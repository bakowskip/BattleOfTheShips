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
	/// <summary>
	/// Opis podsumowujący elementu UnitTest1
	/// </summary>
	[TestClass]
	public class ComputerPlayerTests
	{
		private Mock<IMapPoint> _validPointA;
		private Mock<IMapPoint> _validPointB;
		private Mock<IMapPoint> _invalidPoint;
		private Mock<IGameMap> _gameMap;
		private ComputerPlayer _player;
		public ComputerPlayerTests()
		{
			_validPointA = new Mock<IMapPoint>();
			_validPointA.Setup(p => p.X).Returns(1);
			_validPointA.Setup(p => p.Y).Returns(1);
			_gameMap = new Mock<IGameMap>();
			_gameMap.Setup(m => m.SetupMap(10, 10)).Returns(true);
			_gameMap.Setup(m => m.MaxX).Returns(10);
			_gameMap.Setup(m => m.MaxY).Returns(10);

			_player = new ComputerPlayer(_gameMap.Object);
		}

		private TestContext testContextInstance;

		/// <summary>
		///Pobiera lub ustawia kontekst testu, który udostępnia
		///funkcjonalność i informację o bieżącym przebiegu testu.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
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
		public void ComputerPlayerShipPlacedCorrectly()
		{

		}

		[TestMethod]
		public void ComputerPlayerMultipleShipsPlacedCorrectly()
		{

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
		public void CorrectlyReportsEndofGameForAllShipSank()
		{
			Mock<IShip> shipA = new Mock<IShip>();
			Mock<IShip> shipB = new Mock<IShip>();
			shipA.Setup(s => s.WasHit).Returns(true);
			shipA.Setup(s => s.WasSank).Returns(true);
			shipB.Setup(s => s.WasHit).Returns(true);
			shipB.Setup(s => s.WasSank).Returns(true);

			_player.Ships = new IShip[] {shipA.Object, shipB.Object};

			Assert.IsTrue(_player.IsGameOver());
		}

		[TestMethod]
		public void CorrectlyReportsEndofGameForNotAllShipSank()
		{
			Mock<IShip> shipA = new Mock<IShip>();
			Mock<IShip> shipB = new Mock<IShip>();
			shipA.Setup(s => s.WasHit).Returns(true);
			shipA.Setup(s => s.WasSank).Returns(true);
			shipB.Setup(s => s.WasHit).Returns(true);
			shipB.Setup(s => s.WasSank).Returns(false);

			_player.Ships = new IShip[] { shipA.Object, shipB.Object };

			Assert.IsFalse(_player.IsGameOver());
		}
	}
}
