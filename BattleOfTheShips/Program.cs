using BattleOfTheShipsConsolePresenter;
using BattleOfTheShipsData;
using BattleofTheShipsInterfaces;
using BattleOfTheShipsLogic;
using System;
using System.Collections.Generic;
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
		static void Main(string[] args)
		{
			_presenter = new ConsolePresenter();

			_gameMap = new Map();
			_gameMap.SetupMap(10, 10);


			computerPlayer = new ComputerPlayer(_gameMap);
			computerPlayer.PlaceShips(5, 1);
			computerPlayer.PlaceShips(4, 2);
			
			foreach (IMapPoint mp in _gameMap.MapArea)
			{
				mp.IsHidden = false;mp.WasHit = true;
			}
			_presenter.ShowMap(_gameMap);

			Console.ReadLine();
		}
	}
}
