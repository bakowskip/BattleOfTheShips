using BattleOfTheShipsConsolePresenter;
using BattleOfTheShipsData;
using BattleofTheShipsInterfaces;
using BattleOfTheShipsLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShips
{
	class Program
	{
		private static IGameMap _gameMap;
		private static IPresenter _presenter;
		private static ComputerPlayer computerPlayer;
		private static int _mapXSize;
		private static int _mapYSize;
		private static Dictionary<int, int> shipsConfig;
		static void Main(string[] args)
		{
			LoadConfiguration();

			_presenter = new ConsolePresenter();

			_gameMap = new Map();
			_gameMap.SetupMap(_mapXSize,_mapYSize);


			computerPlayer = new ComputerPlayer(_gameMap);
			try
			{
				foreach (var pair in shipsConfig)
				{
					computerPlayer.PlaceShips(pair.Key, pair.Value);
				}
				
			}
			catch (BattleOfTheShipsData.Exceptions.ShipException shipEx)
			{
				_presenter.ShowMessage("Error while placing ship: " + shipEx.Message, true);
				return;
			}

			string targetInput = string.Empty;
			bool correctTarget;
			while (!computerPlayer.IsGameOver)
			{
				try
				{
					correctTarget = false;
					while (!correctTarget)
					{
						_presenter.ShowMap(_gameMap);

						_presenter.ShowMessage("Please enter coordinates for your next shot in the form of: C4  (or type q! to quit)", false);
						targetInput = Console.ReadLine().ToUpper();

						System.Text.RegularExpressions.Regex regx = new System.Text.RegularExpressions.Regex(@"^[a-wA-W]\d{1,2}$");

						correctTarget = regx.IsMatch(targetInput);
						if (!correctTarget && targetInput == "Q!")
						{
							return;
						}
					}

					var mapTarget = _presenter.ConvertUserInputToMapPoint(targetInput);

					var shotResult = computerPlayer.CheckShot(mapTarget);
					_presenter.ShowHitResult(mapTarget, shotResult.WasHit, shotResult.WasSank);
					_presenter.ShowMessage("(Press any key to continue)", true);
				}
				catch (Exception ex)
				{
					_presenter.ShowMessage("Error while taking a shot: " + ex.Message, true);
				}
			}

			_presenter.ShowMap(_gameMap);
			_presenter.ShowMessage("Good job! You won!",true);
		}


		static void LoadConfiguration()
		{
			shipsConfig = new Dictionary<int, int>();

			var configXSize = ConfigurationManager.AppSettings["MapXSize"];
			var configYSize = ConfigurationManager.AppSettings["MapXSize"];
			var shipsDefinition = ConfigurationManager.AppSettings["ShipSizes"];


			if (!int.TryParse(configXSize, out _mapXSize) || _mapXSize < 1 || _mapXSize > 20)
				_mapXSize = 10;

			if (!int.TryParse(configYSize, out _mapYSize) || _mapYSize < 1 || _mapYSize > 20)
				_mapYSize = 10;

			if (!string.IsNullOrEmpty(shipsDefinition))
			{
				int shipSize = 0;
				int shipCount = 0;

				var splitDefs = shipsDefinition.Split(new char[1] { ';' },StringSplitOptions.RemoveEmptyEntries);
				foreach (string shipDef in splitDefs)
				{
					var splits = shipDef.Split('|');
					if (splits.Length == 2)
					{
						int.TryParse(splits[0], out shipCount);
						int.TryParse(splits[1], out shipSize);
					}
					if (shipSize > 0 && shipCount > 0)
						shipsConfig.Add(shipCount,shipSize);
				}

			}
			else
			{
				shipsConfig.Add(1, 5);
				shipsConfig.Add(3, 4);
			}
		}
	}
}
