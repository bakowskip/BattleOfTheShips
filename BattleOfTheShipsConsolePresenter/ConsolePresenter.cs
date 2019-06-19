using BattleOfTheShipsData;
using BattleOfTheShipsData.Exceptions;
using BattleofTheShipsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleOfTheShipsConsolePresenter
{
	public class ConsolePresenter : IPresenter
	{
		private const int START_CHAR = (int)'A';
		public void ShowMap(IGameMap map)
		{
			Console.Clear();

			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			
			Console.Write("  ");
			for (int i = 0; i < map.MaxX;i++)
			{
				
				Console.Write(" " + Convert.ToChar(START_CHAR + i) + " ");
			}

			Console.WriteLine("");
			
			for (int y = 0; y < map.MaxY; y++)
			{
				Console.BackgroundColor = ConsoleColor.White;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.Write((y + 1).ToString("D2"));

				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				for (int x = 0; x < map.MaxX; x++)
				{
					if (map.MapArea[x, y].IsHidden)
						Console.Write("   ");
					else
					if (map.MapArea[x, y].WasHit)
					{
						if (map.MapArea[x, y].IsShip)
						{
							Console.ForegroundColor = map.MapArea[x, y].Ship.WasSank ? ConsoleColor.Red : ConsoleColor.Yellow;
							if (map.MapArea[x - 1, y].IsShip)
								Console.Write("█");
							else
								Console.Write(" ");

							Console.Write("█");

							if (map.MapArea[x + 1, y].IsShip)
								Console.Write("█");
							else
								Console.Write(" ");
						}
						else
						{
							Console.ForegroundColor = ConsoleColor.Black;
							Console.Write(" o ");
						}
					}
				}
				Console.WriteLine();
			}			
		}

		public void ShowHitResult(IMapPoint target, bool wasHit, bool wasSank)
		{
			var strBuilder = new StringBuilder();
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;

			strBuilder.Append($"Your shot at {Convert.ToChar(START_CHAR + target.X)}{target.Y+1} ");
			if (!wasHit)
				strBuilder.Append("missed");
			else
			{
				if (wasSank)
					strBuilder.Append(" hit and sank a ship!");
				else
					strBuilder.Append(" hit and did not sink a ship!");
			}		

			Console.WriteLine(strBuilder.ToString());
		}

		public IMapPoint ConvertUserInputToMapPoint(string coordinates)
		{
			int x=0,y=0;

			try 
			{
				char cX = coordinates.ToCharArray()[0];				

				x = (int)cX - START_CHAR;
				y = int.Parse(coordinates.Substring(1)) - 1;
			}
			catch 
			{
				throw new MapPointException(x, y, $"Invalit target coordinates: {coordinates}");
			}

			return new MapPoint(x, y);
		}

		public void ShowMessage(string message, bool wait)
		{
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(message);

			if (wait)
				Console.ReadKey();
		}
	}
}
