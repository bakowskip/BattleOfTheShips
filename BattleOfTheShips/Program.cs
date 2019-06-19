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
			_presenter = new ConsolePresenter();

			_gameMap = new Map();
			_gameMap.SetupMap(10, 10);


			computerPlayer = new ComputerPlayer(_gameMap);
			try
			{
				computerPlayer.PlaceShips(1, 5);
				computerPlayer.PlaceShips(3, 4);
			}
			catch (BattleOfTheShipsData.Exceptions.ShipException shipEx)
			{
				Console.WriteLine("Error while placing ship: " + shipEx.Message);
				Console.ReadLine();
				return;
			}
			
			foreach (IMapPoint mp in _gameMap.MapArea)
			{
				mp.IsHidden = false;mp.WasHit = true;
			}
			_presenter.ShowMap(_gameMap);

			Console.ReadLine();
		}


		static void LoadConfiguration()
		{
			shipsConfig = new Dictionary<int, int>();

			var configXSize = ConfigurationManager.AppSettings["MaxXSize"];
			var configYSize = ConfigurationManager.AppSettings["MaxXSize"];
			var shipsDefinition = ConfigurationManager.AppSettings["ShipSizes"];


			if (!int.TryParse(configXSize, out _mapXSize))
				_mapXSize = 10;

			if (!int.TryParse(configYSize, out _mapYSize))
				_mapYSize = 10;

			if (!string.IsNullOrEmpty(shipsDefinition))
			{
				int shipSize = 0;
				int shipCount = 0;

				var splitDefs = shipsDefinition.Split(new char[1] { ';' },StringSplitOptions.RemoveEmptyEntries);


			}
			else
			{
				shipsConfig.Add(1, 5);
				shipsConfig.Add(3, 4);
			}
		}


	}
}
