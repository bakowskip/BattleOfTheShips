using System;
using System.IO;
using System.Text;
using BattleOfTheShipsConsolePresenter;
using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BattleOfTheShipsTests
{
	[TestClass]
	public class ConsolePresenterTests
	{
		static TextWriter originalOutput;
		ConsolePresenter presenter;

		Mock<IMapPoint> shotPoint;
		Mock<IGameMap> gameMap;

		Mock<IMapPoint> emptyPoint;
		Mock<IMapPoint> shotShipPoint;
		Mock<IMapPoint> sankShipPoint;
		Mock<IMapPoint> hiddenPoint;

		Mock<IShip> hitShip;
		Mock<IShip> sankShip;

		[ClassInitialize]
		public static void Init(TestContext context)
		{
			originalOutput = Console.Out;
		}

		[TestInitialize]
		public void Setup()
		{
			presenter = new ConsolePresenter();
			hitShip = new Mock<IShip>();
			hitShip.Setup(_ => _.WasHit).Returns(true);
			hitShip.Setup(_ => _.WasSank).Returns(false);

			sankShip = new Mock<IShip>();
			sankShip.Setup(_ => _.WasHit).Returns(true);
			sankShip.Setup(_ => _.WasSank).Returns(true);

			shotPoint = new Mock<IMapPoint>();
			shotPoint.Setup(_ => _.IsShip).Returns(false);
			shotPoint.Setup(_ => _.WasHit).Returns(false);

			emptyPoint = new Mock<IMapPoint>();
			emptyPoint.Setup(_ => _.IsShip).Returns(false);
			emptyPoint.Setup(_ => _.WasHit).Returns(true);
			emptyPoint.Setup(_ => _.IsBlocked).Returns(false);
			emptyPoint.Setup(_ => _.IsHidden).Returns(false);

			shotShipPoint = new Mock<IMapPoint>();
			shotShipPoint.Setup(_ => _.IsShip).Returns(true);
			shotShipPoint.Setup(_ => _.WasHit).Returns(true);
			shotShipPoint.Setup(_ => _.IsBlocked).Returns(true);
			shotShipPoint.Setup(_ => _.IsHidden).Returns(false);
			shotShipPoint.Setup(_ => _.Ship).Returns(hitShip.Object);

			sankShipPoint = new Mock<IMapPoint>();
			sankShipPoint.Setup(_ => _.IsShip).Returns(true);
			sankShipPoint.Setup(_ => _.WasHit).Returns(true);
			sankShipPoint.Setup(_ => _.IsBlocked).Returns(true);
			sankShipPoint.Setup(_ => _.IsHidden).Returns(false);
			sankShipPoint.Setup(_ => _.Ship).Returns(sankShip.Object);

			hiddenPoint = new Mock<IMapPoint>();
			hiddenPoint.Setup(_ => _.IsShip).Returns(false);
			hiddenPoint.Setup(_ => _.WasHit).Returns(false);
			hiddenPoint.Setup(_ => _.IsBlocked).Returns(false);
			hiddenPoint.Setup(_ => _.IsHidden).Returns(true);

			gameMap = new Mock<IGameMap>();
			gameMap.Setup(_ => _.MaxX).Returns(4);
			gameMap.Setup(_ => _.MaxY).Returns(3);

			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					gameMap.Setup(_ => _[i,j]).Returns(hiddenPoint.Object);
				}
			}

			gameMap.Setup(_ => _[1, 2]).Returns(emptyPoint.Object);
			gameMap.Setup(_ => _[2, 0]).Returns(emptyPoint.Object);
			gameMap.Setup(_ => _[3, 1]).Returns(emptyPoint.Object);
			gameMap.Setup(_ => _[1, 1]).Returns(sankShipPoint.Object);
			gameMap.Setup(_ => _[2, 1]).Returns(shotShipPoint.Object);
		}

		[TestMethod]
		public void MissedShotShownCorrectly()
		{			
			shotPoint.Setup(_ => _.X).Returns(0);
			shotPoint.Setup(_ => _.Y).Returns(1);

			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);

				presenter.ShowHitResult(shotPoint.Object, false, false);

				string expected = string.Format($"Your shot at A2 missed{Environment.NewLine}");
				Assert.AreEqual<string>(expected, sw.ToString());
			}
		}

		[TestMethod]
		public void HitNotSankShotShownCorrectly()
		{
			shotPoint.Setup(_ => _.X).Returns(3);
			shotPoint.Setup(_ => _.Y).Returns(1);
			
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);

				presenter.ShowHitResult(shotPoint.Object, true, false);

				string expected = string.Format($"Your shot at D2 hit but did not sink a ship!{Environment.NewLine}");
				Assert.AreEqual<string>(expected, sw.ToString());
			}
		}

		[TestMethod]
		public void HitAndSankShotShownCorrectly()
		{
			shotPoint.Setup(_ => _.X).Returns(5);
			shotPoint.Setup(_ => _.Y).Returns(6);
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);

				presenter.ShowHitResult(shotPoint.Object, true, true);

				string expected = string.Format($"Your shot at F7 hit and sank a ship!{Environment.NewLine}");
				Assert.AreEqual<string>(expected, sw.ToString());
			}
		}

		[TestMethod]
		public void CustomMessageShown()
		{
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);
				var str = $"Custom test message";
				presenter.ShowMessage(str, false);
				var expected = string.Concat(str, $"{Environment.NewLine}");
				Assert.AreEqual<string>(expected, sw.ToString());
			}
		}

		[TestMethod]
		public void UserInputConvertedToMapPointCorrectly()
		{
			var input = "a1";
			var mp = presenter.ConvertUserInputToMapPoint(input);

			Assert.AreEqual(mp.X, 0);
			Assert.AreEqual(mp.Y, 0);

			input = "c6";
			mp = presenter.ConvertUserInputToMapPoint(input);

			Assert.AreEqual(mp.X, 2);
			Assert.AreEqual(mp.Y, 5);

			input = "K20";
			mp = presenter.ConvertUserInputToMapPoint(input);

			Assert.AreEqual(mp.X, 10);
			Assert.AreEqual(mp.Y, 19);

		}

		[TestMethod]
		public void InvalidUserInputThrowsException()
		{
			var input = "foo";
			Assert.ThrowsException<MapPointException>(() => presenter.ConvertUserInputToMapPoint(input));

			input = "123456";
			Assert.ThrowsException<MapPointException>(() => presenter.ConvertUserInputToMapPoint(input));

			input = "1";
			Assert.ThrowsException<MapPointException>(() => presenter.ConvertUserInputToMapPoint(input));

			input = string.Empty;
			Assert.ThrowsException<MapPointException>(() => presenter.ConvertUserInputToMapPoint(input));

			input = "a";
			Assert.ThrowsException<MapPointException>(() => presenter.ConvertUserInputToMapPoint(input));
		}


		[TestMethod]
		public void MapShownCorrectly()
		{
			using (StringWriter sw = new StringWriter())
			{
				Console.SetOut(sw);
				presenter.ShowMap(gameMap.Object);

				var actual = sw.ToString();

				StringBuilder sb = new StringBuilder();

				sb.AppendLine("   A  B  C  D ");
				sb.AppendLine("01       o    ");
				sb.AppendLine("02    ████  o ");
				sb.AppendLine("03    o       ");

				var expected = sb.ToString();				

				Assert.AreEqual(expected, actual);
			}
		}


		[TestCleanup]
		public void Cleanup()
		{
			Console.SetOut(originalOutput);
		}
	}
}
